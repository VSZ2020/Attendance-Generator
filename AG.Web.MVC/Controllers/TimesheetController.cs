using AG.Core.Enums;
using AG.Core.Extensions;
using AG.Core.Models;
using AG.Core.Models.Timesheet;
using AG.Data;
using AG.Data.Entities;
using AG.Services.Repository;
using AG.Services.Timesheet;
using AG.Services.Utils;
using AG.Web.MVC.Models.Timesheet;
using AG.Web.MVC.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AG.Web.MVC.Controllers
{
    public class TimesheetController : Controller
    {
        #region ctor
        public TimesheetController(DataContext ctx, TimesheetService tss)
        {
            _context = ctx;
            _tss = tss;
        } 
        #endregion

        readonly DataContext _context;
        readonly TimesheetService _tss;

        #region Index
        public async Task<IActionResult> Index(Guid departmentId, int? FilterYear = null, int? FilterMonth = null)
        {
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == departmentId);
            if (department == null)
                return RedirectToAction("Index", "Department");

            var entities = _context.Timesheets
                .AsNoTracking()
                .Include(e => e.Author)
                .Where(e => e.DepartmentId == departmentId);

            if (FilterYear != null)
            {
                entities = entities.Where(e => e.BeginDate.Year == FilterYear.Value);
            }

            if (FilterMonth != null)
            {
                entities = entities.Where(e => e.BeginDate.Month == FilterMonth.Value);
            }

            var timesheets = await entities.Select(e => new TimesheetVM()
            {
                Id = e.Id,
                FormType = e.FormType.GetTimesheetFormName() ?? "-",
                AuthorName = e.Author != null ? e.Author.Username : "-",
                CreatedAt = e.CreatedAt,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Kind = e.Kind,
                HasContent = e.JsonContent != null,
                ResponsibleExecutorName = e.ResponsibleExecutor ?? "-",
                ExecutorName = e.Executor ?? "-",
                AccountingExecutorName = e.AccountingExecutor ?? "-",
                LastModifiedAt = e.LastModifiedAt,
            })
            .OrderByDescending(e => e.BeginDate)
            .ToListAsync();

            return View(new IndexTimesheetVM()
            {
                DepartmentId = departmentId,
                DepartmentName = department!.Name,
                Timesheets = timesheets,
                FilterMonth = FilterMonth,
                FilterYear = FilterYear,
                AvaialbleMonths = PopulateMonths()
            });
        } 
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid departmentId)
        {
            // Obtains default fields of timesheet like ReponsibleExecutor, DepartmentHeader
            var tsDefaults = await _context.TimesheetDefaults.AsNoTracking().FirstOrDefaultAsync(e => e.DepartmentId == departmentId);
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == departmentId);

            if (department == null)
            {
                return NotFound("Подразделение, для которого создается табель, не найдено");
                //return RedirectToAction("Index", "Department");
            }

            var curDate = DateTime.Now;

            return View(new CreateTimesheetVM()
            {
                DepartmentId = departmentId,
                DepartmentName = department!.Name,

                BeginDate = 1,
                EndDate = curDate.Day <= 15 ? 15 : DateTime.DaysInMonth(curDate.Year, curDate.Month),
                Month = curDate.Month,
                Kind = 0,
                FormType = TimesheetFormType.Form0504421,

                ExecutorName = tsDefaults?.Executor ?? "",
                ExecutorFunction = tsDefaults?.ExecutorFunction ?? "",
                ResponsibleExecutorName = tsDefaults?.ResponsibleExecutor ?? "",
                ResponsibleExecutorFunction = tsDefaults?.ResponsibleExecutorFunction ?? "",
                AccountingExecutorName = tsDefaults?.AccountingExecutor ?? "",
                AccountingExecutorFunction = tsDefaults?.AccountingExecutorFunction ?? "",
                DepartmentHeaderName = department?.Header ?? "",

                AvaialbleForms = PopulateTimesheetForms(),
                AvaialbleKinds = PopulateTimesheetKinds(),
                AvaialbleMonths = PopulateMonths(),
                AvailableExecutors = await PopulateExecutorsListAsync(departmentId),
            });
        }

        //[Authorize()]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTimesheetVM model)
        {
            
            var tsDefaults = await _context.TimesheetDefaults.AsNoTracking().FirstOrDefaultAsync(e => e.DepartmentId == model.DepartmentId);
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.DepartmentId);

            if (department == null)
            {
                return NotFound("Подразделение не найдено");
                //return RedirectToAction("Index", "Department");
            }

            if (model.EndDate <  model.BeginDate)
                ModelState.AddModelError("", "Число начала отчетного периода не может быть больше дня окончания отчетного периода");

            var curYear = DateTime.Now.Year;
            var beginDate = new DateTime(curYear, model.Month, model.BeginDate);
            var endDate = new DateTime(curYear, model.Month, model.EndDate);

            var existingEntity = await _context.Timesheets.FirstOrDefaultAsync(
                e => e.DepartmentId == model.DepartmentId &&
                     e.BeginDate == beginDate &&
                     e.EndDate == endDate &&
                     e.Kind == model.Kind &&
                     e.FormType == model.FormType);
            if (existingEntity != null)
                ModelState.AddModelError("","Такой табель уже существует");
            
            if (ModelState.IsValid)
            {
                var userGuid = TryGetUserId();

                var entity = new TimesheetEntity()
                {
                    BeginDate = beginDate,
                    EndDate = endDate,
                    Kind = model.Kind,
                    FormType = model.FormType,
                    Comment = model.Comment,
                    
                    AuthorId = userGuid != Guid.Empty ? userGuid : null,
                    CreatedAt = DateTime.Now,
                    DepartmentId = model.DepartmentId,

                    ResponsibleExecutor = model.ResponsibleExecutorName,
                    ResponsibleExecutorFunction = model.ResponsibleExecutorFunction,

                    Executor = model.ExecutorName,
                    ExecutorFunction = model.ExecutorFunction,

                    AccountingExecutor = model.AccountingExecutorName,
                    AccountingExecutorFunction = model.AccountingExecutorFunction,
                };

                var results = await _context.Timesheets.AddAsync(entity);
                
                try
                {
                    await _context.SaveChangesAsync();

                    await RecreateContentAsync(results.Entity.Id);

                    return RedirectToAction("Index", new { departmentId = model.DepartmentId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать метаданные табеля. Попробуйте позже");
                }
            }


            return View(new CreateTimesheetVM()
            {
                DepartmentId = model.DepartmentId,
                DepartmentName = department!.Name,

                BeginDate = model.BeginDate,
                EndDate = model.EndDate,
                Kind = 0,
                Month = model.Month,

                ExecutorName = tsDefaults?.Executor ?? "",
                ExecutorFunction = tsDefaults?.ExecutorFunction ?? "",
                ResponsibleExecutorName = tsDefaults?.ResponsibleExecutor ?? "",
                ResponsibleExecutorFunction = tsDefaults?.ResponsibleExecutorFunction ?? "",
                AccountingExecutorName = tsDefaults?.AccountingExecutor ?? "",
                AccountingExecutorFunction = tsDefaults?.AccountingExecutorFunction ?? "",
                DepartmentHeaderName = department?.Header ?? "",

                AvaialbleForms = PopulateTimesheetForms(),
                AvaialbleKinds = PopulateTimesheetKinds(),
                AvaialbleMonths = PopulateMonths(),
                AvailableExecutors = await PopulateExecutorsListAsync(model.DepartmentId),
            });
        } 
        #endregion

        #region Remove

        public async Task<IActionResult> Remove(Guid id)
        {
            var entity = await _context.Timesheets.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound("Не найден табель для удаления");

            if (HttpContext.Request.Method == "POST")
            {
                _context.Timesheets.Remove(entity);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    //TODO: Log
                }

                return RedirectToAction("Index", new { departmentId = entity.DepartmentId });
            }
            
            var tsDefaults = await _context.TimesheetDefaults.AsNoTracking().FirstOrDefaultAsync(e => e.DepartmentId == entity.DepartmentId);
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.DepartmentId);

            return View(new RemoveTimesheetVM()
            {
                DepartmentName = department?.Name ?? "-",

                BeginDate = entity.BeginDate.Day,
                EndDate = entity.EndDate.Day,
                Month = entity.BeginDate.Month,
                Kind = 0,

                ExecutorName = tsDefaults?.Executor ?? "",
                ExecutorFunction = tsDefaults?.ExecutorFunction ?? "",
                ResponsibleExecutorName = tsDefaults?.ResponsibleExecutor ?? "",
                ResponsibleExecutorFunction = tsDefaults?.ResponsibleExecutorFunction ?? "",
                AccountingExecutorName = tsDefaults?.AccountingExecutor ?? "",
                AccountingExecutorFunction = tsDefaults?.AccountingExecutorFunction ?? "",
                DepartmentHeaderName = department?.Header ?? "",

                AvaialbleForms = PopulateTimesheetForms(),
                AvaialbleKinds = PopulateTimesheetKinds(),
                AvaialbleMonths = PopulateMonths(),
                AvailableExecutors = await PopulateExecutorsListAsync(entity.DepartmentId),
            });
        }

        #endregion
        
        #region Defaults
        [HttpGet]
        public async Task<IActionResult> Defaults(Guid departmentId)
        {
            var defaults = await _context.TimesheetDefaults
                .AsNoTracking()
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.DepartmentId == departmentId);

            if (defaults == null)
            {
                try
                {
                    var entity = new TimesheetDefaults()
                    {
                        DepartmentId = departmentId,
                    };
                    _context.Add(entity);
                    await _context.SaveChangesAsync();

                    defaults = entity;
                }
                catch (DbUpdateException ex)
                {
                    return NotFound();
                }
            }

            return View(new DefaultsTimesheetVM()
            {
                DepartmentId = defaults.DepartmentId,
                DepartmentName = defaults.Department!.Name,
                ResponsibleExecutorName = defaults.ResponsibleExecutor,
                ResponsibleExecutorFunction = defaults.ResponsibleExecutorFunction,

                ExecutorName = defaults.Executor,
                ExecutorFunction = defaults.ExecutorFunction,

                AccountingExecutorName = defaults.AccountingExecutor,
                AccountingExecutorFunction = defaults.AccountingExecutorFunction,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Defaults(DefaultsTimesheetVM model)
        {
            var entity = await _context.TimesheetDefaults
                .AsNoTracking()
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.DepartmentId == model.DepartmentId);

            if (entity == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                entity.ResponsibleExecutor = model.ResponsibleExecutorName;
                entity.ResponsibleExecutorFunction = model.ResponsibleExecutorFunction;
                entity.Executor = model.ExecutorName;
                entity.ExecutorFunction = model.ExecutorFunction;
                entity.AccountingExecutor = model.AccountingExecutorName;
                entity.AccountingExecutorFunction = model.AccountingExecutorFunction;

                _context.Update(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { departmentId = model.DepartmentId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось обновить данные. Попробуйте позже или обратитесь к администратору");
                }
            }

            return View(new DefaultsTimesheetVM()
            {
                DepartmentId = model.DepartmentId,
                DepartmentName = model.DepartmentName,
                ResponsibleExecutorName = model.ResponsibleExecutorName,
                ResponsibleExecutorFunction = model.ResponsibleExecutorFunction,

                ExecutorName = model.ExecutorName,
                ExecutorFunction = model.ExecutorFunction,

                AccountingExecutorName = model.AccountingExecutorName,
                AccountingExecutorFunction = model.AccountingExecutorFunction,
            });
        }
        #endregion

        #region RecreateContentAsync
        [HttpGet]
        public async Task<IActionResult> RecreateContentAsync(Guid id)
        {
            var entity = await _context.Timesheets.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound("Табель не обнаружен");

            await RecreateTimesheetContentAsync(entity);

            return RedirectToAction("Content", "Timesheet", new {id});
        } 
        #endregion

        [HttpGet]
        public async Task<IActionResult> ToExcelAsync(Guid id)
        {
            var entity = await _context.Timesheets.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound("Табель не обнаружен");

            return View();
        }

        #region Content
        [HttpGet]
        public async Task<IActionResult> Content(Guid id)
        {
            var entity = await _context.Timesheets.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound("Табель не обнаружен");

            if (!string.IsNullOrEmpty(entity.JsonContent))
            {
                var timesheetContent = _tss.DeserializeTimesheet(entity.JsonContent);
                if (timesheetContent != null)
                {
                    return View(ToFormContentViewModel(id, timesheetContent));
                }
            }

            var model = ToFormContentViewModel(entity.Id, await ToFormContentAsync(entity, null));
            return View(model);
        }
        #endregion

        #region Populate functions
        private SelectList PopulateMonths() => new SelectList(CommonLists.Months, "Key","Value");

        private async Task<SelectList> PopulateExecutorsListAsync(Guid departmentId)
        {
            var entities = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Employee)
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();

            return new SelectList(
                entities.Select(e => new { Id = NameUtils.ToLongName(e.Employee), FullName = NameUtils.ToLongName(e.Employee) }).ToList(),
                "Id", "FullName");
        }

        private SelectList PopulateTimesheetKinds()
        {
            var items = new Dictionary<int, string>(){
                { 0, "Первичный" },
                { 1, "Корректирующий-1" },
                { 2, "Корректирующий-2" },
                { 3, "Корректирующий-3" },
                { 4, "Корректирующий-4" },
                { 5, "Корректирующий-5" },
            };

            return new SelectList(items, "Key","Value");
        }

        private SelectList PopulateTimesheetForms()
        {
            var items = new Dictionary<TimesheetFormType, string>(){
                { TimesheetFormType.Form0504421, "№ 0504421" },
                // { TimesheetFormType.FormT12, "№ Т-12" },
                // { TimesheetFormType.FormT13, "№ Т-13" },
            };
            return new SelectList(items, "Key", "Value");
        } 
        #endregion
        
        private async Task<bool> RecreateTimesheetContentAsync(TimesheetEntity entity) 
        {
            var rows = await _tss.GetForm0504421Rows(entity.DepartmentId, entity.BeginDate.Year, entity.BeginDate.Month, entity.BeginDate.Day, entity.EndDate.Day);
            var content = await ToFormContentAsync(entity, rows);

            entity.JsonContent = _tss.SerializeTimesheet(content);
            _context.Timesheets.Update(entity);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                //TODO: add logger
            }
            return false;
        }

        private Form0504421ContentVM ToFormContentViewModel(Guid id, Form0504421Content content)
        {
            var dateBegin = content.Begin.Day;
            var dateEnd = content.End.Day;
            List<DateTime> dates = Enumerable.Range(0, dateEnd - dateBegin + 1).Select(i => content.Begin.AddDays(i)).ToList();

            return new Form0504421ContentVM()
            {
                Id = id,
                EstablishmentName = content.Establishment,
                DepartmentName = content.Department,

                Begin = content.Begin,
                End = content.End,

                TimesheetKind = content.CorrectionNumber.ToString(),
                TimesheetNumber = "",
                Rows = ToRowViewModel(content.Rows),

                //TODO: Add new field to Entity: LastModifiedAt
                LastModifiedAt = content.CreationDate,

                Dates = dates,
                ResponsibleEmployee = content.ResponsibleEmployee,
                ResponsibleEmployeeFunction = content.ResponsibleEmployeeFunction,
                ExecutiveEmployee = content.ExecutiveEmployee,
                ExecutiveEmployeeFunction = content.ExecutiveEmployeeFunction,
                AccountingEmployee = content.AccountingEmployee,
                AccountingEmployeeFunction = content.AccountingEmployeeFunction,
            };
        }

        private async Task<Form0504421Content> ToFormContentAsync(TimesheetEntity entity, List<Form0504421Row>? rows)
        {
            var department = await _context.Departments.Include(e => e.Establishment).FirstOrDefaultAsync(e => e.Id == entity.DepartmentId);
            var estabName = department?.Establishment?.ShortName ?? "";
            var depName = department?.Name ?? "";

            var dateBegin = entity.BeginDate.Day;
            var dateEnd = entity.EndDate.Day;

            return new Form0504421Content()
            {
                CorrectionNumber = entity.Kind,
                TimesheetNumber = "",
                TimesheetType = entity.FormType!.Value,
                Establishment = estabName,
                Department = depName,
                Begin = entity.BeginDate,
                End = entity.EndDate,
                CreationDate = entity.CreatedAt,

                Rows = rows,

                ResponsibleEmployee = entity.ResponsibleExecutor,
                ResponsibleEmployeeFunction = entity.ResponsibleExecutorFunction,
                AccountingEmployee = entity.AccountingExecutor,
                AccountingEmployeeFunction = entity.AccountingExecutorFunction,
                ExecutiveEmployee = entity.Executor,
                ExecutiveEmployeeFunction = entity.ExecutorFunction,
            };
        }

        private List<Form0504421RowVM> ToRowViewModel(List<Form0504421Row>? rows)
        {
            return rows?.Select(r => new Form0504421RowVM()
            {
                // Преобразование в модель вида
                Days = r.Days.Select(d => new DayVM()
                {
                    Text = TimeIntervalService.GetTimeIntervalShortName(d),
                }).ToList(),
                IsConcurrent = r.IsConcurrent,
                EmployeeName = r.EmployeeName,
                Function = r.Function,
                Number = r.Number,
                Rate = r.Rate,

                TotalHoursPerHalfMonth = TimesheetService.EvaluateHalfMonthHours(r.Days),
                TotalHoursPerFullMonth = r.Days.Sum(e => e.Hours),
            }).ToList() ?? new List<Form0504421RowVM>();
        }

        private Guid TryGetUserId()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
            var userGuid = Guid.Empty;
            Guid.TryParse(userId, out userGuid);
            return userGuid;
        }
    }
}
