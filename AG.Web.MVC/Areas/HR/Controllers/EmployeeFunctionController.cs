using AG.Data;
using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;
using AG.Services.Repository;
using AG.Services.Utils;
using AG.Web.MVC.Areas.HR.Models.EmployeeFunction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    public class EmployeeFunctionController : Controller
    {
        public EmployeeFunctionController(DataContext ctx, EmployeeFunctionService efs)
        {
            _context = ctx;
            _functionService = efs;
        }

        readonly DataContext _context;
        readonly EmployeeFunctionService _functionService;

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(Guid employeeId)
        {
            var employee = await _context.Employees.AsNoTracking().Where(e => e.Id == employeeId).FirstOrDefaultAsync();
            if (employee == null)
                return NotFound(); //or Redirect to other page

            var emplDep = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Function)
                .Include(e => e.Department)
                .Where(e => e.EmployeeId == employeeId)
                .ToArrayAsync();

            var emplFuncVM = emplDep.Select(e => new EmployeeFunctionVM()
            {
                Id = e.Id,
                FunctionName = e.Function!.Name,
                DepartmentName = e.Department!.Name,
                Rate = e.Rate,
                AssignmentDate = e.AssignmentDate,
                FiredDate = e.FiredDate,
                IsConcurrent = e.IsConcurrent,
            });

            return View(new EmployeeFunctionIndexVM()
            {
                EmployeeId = employeeId,
                EmployeeName = NameUtils.ToLongName(employee!.LastName, employee.FirstName, employee.MiddleName),
                EmployeeFunctions = emplFuncVM,
            });
        }
        #endregion


        #region Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid employeeId)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
                return NotFound();

            return View(new CreateEmployeeFunctionVM()
            {
                EmployeeName = NameUtils.ToLongName(employee.LastName, employee.FirstName, employee.MiddleName),
                AvailableDepartments = await PopulateDepartmentsListAsync(),
                AvailableFunctions = await PopulateFunctionsListAsync(),
                AvailableSchedules = await PopulateSchedulesListAsync(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeFunctionVM emplFun)
        {
            var validationMsg = await _functionService.ValidateEmployeeFunctionAsync(emplFun.EmployeeId, emplFun.DepartmentId, emplFun.FunctionId, emplFun.ScheduleId, emplFun.AssignmentDate, emplFun.FiredDate);
            if (!string.IsNullOrEmpty(validationMsg))
            {
                ModelState.AddModelError("", validationMsg);
            }

            if (ModelState.IsValid)
            {
                var entity = new EmployeeToDepartment()
                {
                    EmployeeId = emplFun.EmployeeId,
                    DepartmentId = emplFun.DepartmentId,
                    FunctionId = emplFun.FunctionId,
                    ScheduleId = emplFun.ScheduleId,
                    AssignmentDate = emplFun.AssignmentDate,
                    Rate = emplFun.Rate,
                    FiredDate = emplFun.FiredDate,
                    IsConcurrent = emplFun.IsConcurrent,
                    Reason = emplFun.Reason,
                };

                await _context.EmplToDepTable.AddAsync(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = entity.EmployeeId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать запись. Обратитесь к администратору или повторите попытку позже");
                }
            }

            return View(new CreateEmployeeFunctionVM()
            {
                AvailableDepartments = await PopulateDepartmentsListAsync(),
                AvailableFunctions = await PopulateFunctionsListAsync(),
                AvailableSchedules = await PopulateSchedulesListAsync(),
            });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid recordId)
        {
            var entity = await _context.EmplToDepTable.AsNoTracking().FirstOrDefaultAsync(e => e.Id == recordId);
            if (entity == null)
                return NotFound();

            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity!.EmployeeId);
            if (employee == null)
                return NotFound();

            return View(new EditEmployeeFunctionVM()
            {
                Id = entity.Id,
                EmployeeId = entity.EmployeeId,
                EmployeeName = NameUtils.ToLongName(employee.LastName, employee.FirstName, employee.MiddleName),
                DepartmentId = entity.DepartmentId,
                FunctionId = entity.FunctionId,
                ScheduleId = entity.ScheduleId,
                AssignmentDate = entity.AssignmentDate,
                FiredDate = entity.FiredDate,
                IsConcurrent = entity.IsConcurrent,
                Reason = entity.Reason,
                Rate = entity.Rate,
                AvailableDepartments = await PopulateDepartmentsListAsync(),
                AvailableFunctions = await PopulateFunctionsListAsync(),
                AvailableSchedules = await PopulateSchedulesListAsync(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeFunctionVM emplFun)

        {
            var entity = await _context.EmplToDepTable.FirstOrDefaultAsync(e => e.Id == emplFun.Id);
            if (entity == null)
                return NotFound();

            var validationMsg = await _functionService.ValidateEmployeeFunctionAsync(
                entity.EmployeeId,
                emplFun.DepartmentId,
                emplFun.FunctionId,
                emplFun.ScheduleId,
                emplFun.AssignmentDate, emplFun.FiredDate,
                entity.Id);

            if (!string.IsNullOrEmpty(validationMsg))
            {
                ModelState.AddModelError("", validationMsg);
            }

            if (ModelState.IsValid)
            {

                entity.DepartmentId = emplFun.DepartmentId;
                entity.FunctionId = emplFun.FunctionId;
                entity.ScheduleId = emplFun.ScheduleId;
                entity.AssignmentDate = emplFun.AssignmentDate;
                entity.Rate = emplFun.Rate;
                entity.FiredDate = emplFun.FiredDate;
                entity.IsConcurrent = emplFun.IsConcurrent;
                entity.Reason = emplFun.Reason;

                _context.EmplToDepTable.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = entity.EmployeeId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать запись. Обратитесь к администратору или повторите попытку позже");
                }
            }

            emplFun.AvailableDepartments = await PopulateDepartmentsListAsync();
            emplFun.AvailableFunctions = await PopulateFunctionsListAsync();
            emplFun.AvailableSchedules = await PopulateSchedulesListAsync();

            return View(emplFun);
        }
        #endregion

        #region Fire
        [HttpGet]
        public async Task<IActionResult> Fire(Guid recordId)
        {

            var entity = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Function)
                .Include(e => e.Employee)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == recordId);
            if (entity == null)
                return NotFound("Запись не существует");

            var employee = entity.Employee!;
            return View(new FireEmployeeFunctionVM()
            {
                RecordId = entity.Id,
                EmployeeId = entity.EmployeeId,

                FunctionName = entity.Function!.Name,
                DepartmentName = entity.Department!.Name,
                EmployeeName = NameUtils.ToLongName(employee.LastName, employee.FirstName, employee.MiddleName),
                FiredDate = DateTime.Now.Date,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Fire(FireEmployeeFunctionVM model)
        {
            var entity = await _context.EmplToDepTable
                .Include(e => e.Function)
                .Include(e => e.Employee)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == model.RecordId);
            if (entity == null)
                return NotFound("Запись не существует");

            if (entity.FiredDate.HasValue)
                return RedirectToAction("Index", new {employeeId = entity.EmployeeId });

            if (model.FiredDate.HasValue && model.FiredDate.Value.Date < entity.AssignmentDate.Date)
                ModelState.AddModelError("", "Дата увольнения не может предшествовать дате назначения на должность");

            if (ModelState.IsValid)
            {
                entity.FiredDate = model.FiredDate;
                _context.EmplToDepTable.Update(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = entity.EmployeeId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось применить изменения. Попробуйте позже или обратитесь к администратору");
                }
            }

            var employee = entity.Employee!;
            return View(new FireEmployeeFunctionVM()
            {
                RecordId = entity.Id,

                FunctionName = entity.Function!.Name,
                DepartmentName = entity.Department!.Name,
                EmployeeName = NameUtils.ToLongName(employee.LastName, employee.FirstName, employee.MiddleName),
            });
        }
        #endregion

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(Guid recordId)
        {
            var entity = await _context.EmplToDepTable
                .Include(e => e.Employee)
                .Include(e => e.Function)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == recordId);
            if (entity == null)
                return NotFound();

            var count = await _context.EmplToDepTable
                .AsNoTracking()
                .Where(e => e.EmployeeId == entity.EmployeeId)
                .CountAsync();
            if (count == 1)
                return RedirectToAction("Error",new {message = "Как минимум одна должность сотрудника должна остаться" });

            if (Request.Method == "POST")
            {
                _context.Remove(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { employeeId = entity.EmployeeId });
                }
                catch(DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось удалить должность сотрудника. Попробуйте позже или обратитесь к администратору");
                }
                
            }

            return View(new RemoveEmployeeFunctionVM
            {
                RecordId = entity.Id,
                EmployeeId = entity.EmployeeId,
                DepartmentName = entity.Department!.Name,
                FunctionName = entity.Function!.Name,
                EmployeeName = NameUtils.ToLongName(entity.Employee!.LastName, entity.Employee.FirstName, entity.Employee.MiddleName),
            });
        }



        public IActionResult Error(string message, string? action = null, string? controller = null)
        {
            ViewBag.Message = message;

            if (!string.IsNullOrEmpty(action))
                ViewBag.Action = action;
            if (!string.IsNullOrEmpty(controller))
                ViewBag.Controller = controller;
            return View();
        }

        #region Populate methods
        private async Task<SelectList> PopulateDepartmentsListAsync()
        {
            var entities = await _context.Departments.AsNoTracking().ToArrayAsync();
            return new SelectList(entities, nameof(DepartmentEntity.Id), nameof(DepartmentEntity.Name));
        }

        private async Task<SelectList> PopulateFunctionsListAsync()
        {
            var entities = await _context.Functions.AsNoTracking().ToArrayAsync();
            return new SelectList(entities, nameof(FunctionEntity.Id), nameof(FunctionEntity.Name), entities.FirstOrDefault(), nameof(FunctionEntity.Category));
        }

        private async Task<SelectList> PopulateSchedulesListAsync()
        {
            var entities = await _context.Schedules.AsNoTracking().ToArrayAsync();
            return new SelectList(entities, nameof(ScheduleEntity.Id), nameof(ScheduleEntity.Title));
        }
        #endregion
    }
}
