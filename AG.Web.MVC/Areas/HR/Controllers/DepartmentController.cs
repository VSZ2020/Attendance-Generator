using AG.Data;
using AG.Data.Defaults;
using AG.Data.Entities;
using AG.Services.Utils;
using AG.Web.MVC.Areas.HR.Models.Department;
using AG.Web.MVC.Areas.HR.Models.EmployeeFunction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    public class DepartmentController : Controller
    {
        #region ctor
        public DepartmentController(DataContext ctx)
        {
            _context = ctx;
            _defaulEstablishment = DefaultEntities.DEFAULT_ESTABLISHMENT_ID;
        }
        #endregion

        readonly DataContext _context;
        readonly Guid _defaulEstablishment;

        #region Index
        //[Authorize(Policy = "AdminModHR")]
        public IActionResult Index()
        {
            var entities = _context.Departments.AsNoTracking().ToList();

            var departments = entities.Select(e => new DepartmentViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Header = e.Header ?? "Не назначен",
                EmployeesCount = _context.EmplToDepTable.AsNoTracking().Where(t => t.DepartmentId == e.Id).Select(t => t.EmployeeId).Distinct().Count(),
            }).ToList();

            return View(new DepartmentListViewModel() { Departments = departments });
        } 
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var headers = await PopulateHeadersListAsync();
            var department = new DepartmentViewModel() { Name = "Новое подразделение", Header = "", HeadersList = headers };
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(nameof(DepartmentViewModel.Name), nameof(DepartmentViewModel.Header))] DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                var defaultsEntity = new TimesheetDefaults();
                await _context.TimesheetDefaults.AddAsync(defaultsEntity);

                var depEntity = new DepartmentEntity()
                {
                    EstablishmentId = _defaulEstablishment,
                    Name = department.Name,
                    Header = department.Header,
                    TimesheetsDefaults = defaultsEntity
                };
                await _context.Departments.AddAsync(depEntity);

                try
                {
                    //TODO: Fix saving error
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось добавить подразделение. Попробуйте позже");
                }
            }
            department.HeadersList = await PopulateHeadersListAsync();
            return View(department);
        }
        #endregion


        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var entity = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == Id);
            if (entity == null)
                return NotFound();

            var headers = await PopulateHeadersListAsync();
            var department = new DepartmentViewModel() { Id = entity.Id, Name = entity.Name, Header = entity.Header ?? "", HeadersList = headers };
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name, string? Header)
        {
            var validResult = await HasHeaderOtherAssignment(Header);
            if (validResult.isAssigned)
                ModelState.AddModelError("Header", $"Cотрудник {Header} уже является руководителем другого подразделения ({validResult.departmentName}). Выберите другого сотрудника или снимите текущего с должности в подразделении {validResult.departmentName}");

            if (ModelState.IsValid)
            {
                var entity = await _context.Departments.FindAsync(Id);
                if (entity == null)
                    return NotFound();

                entity.Name = Name;
                entity.Header = Header;

                _context.Departments.Update(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException uEx)
                {
                    ModelState.AddModelError("", "Не удалось сохранить изменения. Попробуйте позже");
                }
            }

            var headers = await PopulateHeadersListAsync();
            var department = new DepartmentViewModel() { Id = Id, Name = Name, Header = Header ?? "", HeadersList = headers };
            return View(department);
        }
        #endregion


        #region Remove
        //[Authorize(Policy = "AdminMod")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var entity = await _context.Departments.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return NotFound("Подразделение не найдено");

            if (Request.Method == "POST")
            {
                _context.Departments.Remove(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось удалить подразделение. Попробуйте позже или обратитесь к администратору");
                }
            }

            var relatedEntities = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Function)
                .Include(e => e.Employee)
                .Where(e => e.DepartmentId == entity.Id)
                .Select(e => new EmployeeFunctionToRemoveVM()
                {
                    EmployeeName = NameUtils.ToLongName(e.Employee!),
                    FunctionName = e.Function!.Name,
                    AssignedDate = e.AssignmentDate,
                    FiredDate = e.FiredDate,
                    IsConcurrent = e.IsConcurrent,
                    Rate = e.Rate,
                })
                .ToListAsync();

            return View(new RemoveDepartmentVM()
            {
                DepartmentId = entity.Id,
                DepartmentName = entity.Name,
                FunctionsToRemove = relatedEntities,
            });
        }
        #endregion


        #region PopulateHeadersListAsync
        /// <summary>
        /// Populates list of headers short names
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<string>> PopulateHeadersListAsync()
        {
            var headers = await _context.Employees.AsNoTracking().Select(h => NameUtils.ToShortName(h.LastName, h.FirstName, h.MiddleName)).ToListAsync();
            headers.Insert(0, "Не выбран");
            return headers;
        }
        #endregion

        #region HasHeaderOtherAssignment
        /// <summary>
        /// Check whether employee has other assigned department with header role
        /// </summary>
        /// <param name="headerShortName"></param>
        /// <returns></returns>
        private async Task<(bool isAssigned, string? departmentName)> HasHeaderOtherAssignment(string headerShortName)
        {
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Header == headerShortName);
            if (department == null)
                return (false, null);
            return (true, department.Name);
        } 
        #endregion
    }
}
