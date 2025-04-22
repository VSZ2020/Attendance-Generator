using AG.Core.Models;
using AG.Data;
using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;
using AG.Services.Repository;
using AG.Services.Timesheet;
using AG.Services.Utils;
using AG.Web.MVC.Models.EmployeeTimeInterval;
using AG.Web.MVC.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Controllers
{
    public class EmployeeTimeIntervalController : Controller
    {
        #region ctor

        public EmployeeTimeIntervalController(ILogger<EmployeeTimeIntervalController> logger,DataContext ctx)
        {
            _context = ctx;
            _logger = logger;
        }

        #endregion

        readonly DataContext _context;
        readonly ILogger<EmployeeTimeIntervalController> _logger;

        #region Index
        //[Authorize(Policy = "AdminModHR")]
        public async Task<IActionResult> Index(Guid employeeId, int? FilterMonth = null, int? FilterYear = null, Guid? redirectDepartmentId = null)
        {
            var entities = _context.EmplToTimeInt
                .AsNoTracking()
                .Where(e => e.EmployeeId == employeeId);

            if (FilterYear != null)
            {
                entities = entities.Where(i => i.Begin.Year == FilterYear.Value);
            }

            if (FilterMonth != null)
            {
                entities = entities.Where(i => i.Begin.Month == FilterMonth.Value);
            }

            var intervals = await entities
                .Select(e => new EmployeeTimeIntervalVM()
                {
                    Id = e.Id,
                    Title = TimeIntervalService.TimeIntervalsDict[e.TimeIntervalType].Title,
                    ShortTitle = TimeIntervalService.TimeIntervalsDict[e.TimeIntervalType].ShortTitle,
                    Begin = e.Begin,
                    End = e.End,
                })
                .OrderByDescending(e => e.Begin)
                .ToListAsync();

            //EmployeeInfo
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == employeeId);

            var vm = new IndexEmployeeTimeIntervalVM()
            {
                EmployeeId = employeeId,
                EmployeeName = employee != null ? NameUtils.ToLongName(employee.LastName, employee.FirstName, employee.MiddleName) : "",
                FilterMonth = FilterMonth,
                FilterYear = FilterYear,
                TimeIntervals = intervals,
                AvailableMonths = PopulateMonths(),
                RedirectDepartmentId = redirectDepartmentId
            };
            return View(vm);
        } 
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create(Guid employeeId)
        {
            var timeIntervals = PopulateTimeIntervalsList();
            return View(new CreateEmployeeTimeIntervalVM()
            {
                Begin = DateTime.Now.Date,
                End = DateTime.Now.Date.AddDays(10),
                EmployeeId = employeeId,
                TimeIntervals = timeIntervals,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind(
                nameof(CreateEmployeeTimeIntervalVM.EmployeeId),
                nameof(CreateEmployeeTimeIntervalVM.Begin),
                nameof(CreateEmployeeTimeIntervalVM.End), 
                nameof(CreateEmployeeTimeIntervalVM.SelectedTimeIntervalType))]
            CreateEmployeeTimeIntervalVM employeeTi)
        {
            if (employeeTi.Begin > employeeTi.End)
                ModelState.AddModelError("", "Начальная дата не может быть меньше конечной");

            if (ModelState.IsValid)
            {
                var entity = new EmployeeToTimeInterval()
                {
                    EmployeeId = employeeTi.EmployeeId,
                    Begin = employeeTi.Begin,
                    End = employeeTi.End,
                    TimeIntervalType = employeeTi.SelectedTimeIntervalType,
                };

                _context.EmplToTimeInt.Add(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = employeeTi.EmployeeId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать запись. Обратитесь к администратору или попробуйте позже");
                    _logger.LogError(ex, "Can't create employee time interval");
                }
            }
            employeeTi.TimeIntervals = PopulateTimeIntervalsList();
            return View(employeeTi);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await _context.EmplToTimeInt.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound();

            var timeIntervals = PopulateTimeIntervalsList();
            return View(new EditEmployeeTimeIntervalVM()
            {
                Id = entity.Id,
                Begin = entity.Begin,
                End = entity.End,
                EmployeeId = entity.EmployeeId,
                TimeIntervalTitle = TimeIntervalService.TimeIntervalsDict[entity.TimeIntervalType].Title,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            [Bind("Id","EmployeeId","Begin","End")]
            EditEmployeeTimeIntervalVM employeeTi)
        {
            if (employeeTi.Begin > employeeTi.End)
                ModelState.AddModelError("", "Начальная дата не может быть меньше конечной");

            if (ModelState.IsValid)
            {
                var entity = await _context.EmplToTimeInt.FirstOrDefaultAsync(e => e.Id == employeeTi.Id && e.EmployeeId == employeeTi.EmployeeId);
                if (entity == null)
                    return NotFound();

                entity.Begin = employeeTi.Begin;
                entity.End = employeeTi.End;

                _context.EmplToTimeInt.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = employeeTi.EmployeeId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось обновить запись. Обратитесь к администратору или попробуйте позже");
                    _logger.LogError(ex, "Can't edit employee time interval");
                }
            }
            return View(employeeTi);
        } 
        #endregion

        #region Remove

        public async Task<IActionResult> Remove(Guid id)
        {
            var entity = await _context.EmplToTimeInt.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound();

            if (HttpContext.Request.Method == "POST")
            {
                try
                {
                    _context.EmplToTimeInt.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    _logger.LogError(e, "Can't remove employee time interval");
                }
                return RedirectToAction("Index", new { employeeId = entity.EmployeeId });
            }
            
            return View(new RemoveEmployeeTimeIntervalVM()
            {
                Id = entity.Id,
                Begin = entity.Begin,
                End = entity.End,
                EmployeeId = entity.EmployeeId,
                IntervalName = TimeIntervalService.TimeIntervalsDict[entity.TimeIntervalType].Title,
            });
        }

        #endregion
        

        #region Populate functions

        private SelectList PopulateTimeIntervalsList()
        {
            var entities = TimeIntervalService.TimeIntervals.OrderByDescending(e => e.Title);
            return new SelectList(entities, nameof(TimeInterval.DayType), nameof(TimeInterval.Title));
        }

        private List<SelectListItem> PopulateMonths() => CommonLists.Months.Select(e => new SelectListItem(e.Value, e.Key.ToString())).ToList();

        #endregion

    }
}
