using AG.Data;
using AG.Data.Entities;
using AG.Data.Entities.RelationshipTables;
using AG.Services.Utils;
using AG.Web.MVC.Areas.HR.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Areas.HR.Controllers
{
    [Area("HR")]
    public class EmployeeController : Controller
    {
        #region ctor
        public EmployeeController(DataContext ctx)
        {
            _context = ctx;
        }
        #endregion

        readonly DataContext _context;


        #region ByDepartment
        public async Task<IActionResult> ByDepartment()
        {
            var emplWithDeps = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .Include(e => e.Function)
                .GroupBy(e => e.DepartmentId)
                .ToArrayAsync();

            var departments = await _context.Departments.AsNoTracking().ToDictionaryAsync(x => x.Id);

            var groups = emplWithDeps.Select(e => new EmployeeGroupViewModel()
            {
                DepartmentName = departments[e.Key].Name,
                DepartmentId = e.Key,
                Employees = e.Select(t => new EmployeeViewModel()
                {
                    Id = t.EmployeeId,
                    LastName = t.Employee!.LastName,
                    FirstName = t.Employee.FirstName,
                    MiddleName = t.Employee.MiddleName,
                    Rate = t.Rate,
                    Function = t.Function!.Name,
                }).ToList()
            }).ToList();

            var vm = new EmployeeByDepViewModel()
            {
                EmployeeGroups = groups
            };
            return View(vm);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(Guid departmentId)
        {
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(e => e.Id == departmentId);
            if (department == null)
                return NotFound("Подразделение не найдено");

            var entities = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .Include(e => e.Function)
                .Where(e => e.DepartmentId == departmentId)
                .ToArrayAsync();

            var employees = entities.Select(t => new EmployeeViewModel()
            {
                Id = t.EmployeeId,
                LastName = t.Employee!.LastName,
                FirstName = t.Employee.FirstName,
                MiddleName = t.Employee.MiddleName,
                Rate = t.Rate,
                Function = t.Function!.Name,
            }).ToList();

            var vm = new EmployeeIndexViewModel()
            {
                DepartmentName = department?.Name ?? "Все подразделения",
                DepartmentId = departmentId,
                Employees = employees,
            };
            return View(vm);
        }
        #endregion

        #region AllEmployees
        public async Task<IActionResult> AllEmployees(string? FilterName = null)
        {
            var request = _context.Employees.AsNoTracking();
            var entities = await request.OrderBy(e => e.LastName).OrderBy(e => e.FirstName).OrderBy(e => e.MiddleName).ToArrayAsync();

            if (FilterName != null)
            {
                var phrase = FilterName;
                entities = entities.Where(e => NameUtils.ToLongName(e).Contains(phrase, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            }

            

            var employees = entities.Select(t => new EmployeeViewModel()
            {
                Id = t.Id,
                LastName = t.LastName,
                FirstName = t.FirstName,
                MiddleName = t.MiddleName,
                Email = t.Email,
                Phone = t.Phone,
            }).ToList();

            var vm = new EmployeeIndexViewModel()
            {
                DepartmentName = "Все подразделения",
                DepartmentId = Guid.Empty,
                Employees = employees,
            };
            return View(vm);
        } 
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create(Guid departmentId)
        {
            var functions = await PopulateFunctionsListAsync();
            var departments = await PopulateDepartmentsListAsync();
            return View(new CreateEmployeeViewModel()
            {

                DepartmentId = departmentId,
                AvailableFunctions = functions,
                AvailableDepartments = departments,
                Rate = 1.0F,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel employee)
        {
            //Check if employee already works in this department
            var emplRecord = await _context.EmplToDepTable
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.Employee!.LastName == employee.LastName && e.Employee.FirstName == employee.FirstName && e.Employee.MiddleName == employee.MiddleName && e.DepartmentId == employee.DepartmentId);
            if (emplRecord != null)
                ModelState.AddModelError("", "Такой сотрудник уже есть в системе. Попробуйте вместо создания новой записи сотрудника добавить ему новую должность в списке должностей. Если у вас нет нужных прав, обратитесь в отдел кадров.");


            if (ModelState.IsValid)
            {
                var entity = new EmployeeEntity()
                {
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                };

                var relEntity = new EmployeeToDepartment()
                {
                    Employee = entity,
                    DepartmentId = employee.DepartmentId,
                    AssignmentDate = employee.AssignmentDate,
                    IsConcurrent = employee.IsConcurrent,
                    FunctionId = employee.FunctionId,
                    Reason = employee.Reason,
                    Rate = employee.Rate,
                };

                _context.Employees.Add(entity);
                _context.EmplToDepTable.Add(relEntity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Employee", new { departmentId = employee.DepartmentId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать новую запись. Обратитесь к администратору или попробуйте позже");
                }
            }

            employee.AvailableFunctions = await PopulateFunctionsListAsync();
            employee.AvailableDepartments = await PopulateDepartmentsListAsync();
            return View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid employeeId, Guid departmentId)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (entity == null)
                return NotFound();

            return View(new EditEmployeeViewModel()
            {
                Id = employeeId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                Email = entity.Email,
                Phone = entity.Phone,
                DepartmentId = departmentId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var entity = await _context.Employees.FindAsync(employee.Id);

                if (entity == null)
                    return NotFound();

                entity.LastName = employee.LastName;
                entity.FirstName = employee.FirstName;
                entity.MiddleName = employee.MiddleName;
                entity.Email = employee.Email;
                entity.Phone = employee.Phone;

                _context.Employees.Update(entity);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Employee", new { departmentId = employee.DepartmentId });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Не удалось создать новую запись. Обратитесь к администратору или попробуйте позже");
                }
            }

            return View(employee);
        }
        #endregion

        #region Populate functions
        private async Task<SelectList> PopulateFunctionsListAsync()
        {
            var entities = await _context.Functions.AsNoTracking().ToListAsync();
            return new SelectList(entities, "Id", "Name", entities.FirstOrDefault(), nameof(FunctionEntity.Category));
        }

        private async Task<SelectList> PopulateDepartmentsListAsync()
        {
            var entities = await _context.Departments.AsNoTracking().ToListAsync();
            return new SelectList(entities, "Id", "Name");
        }
        #endregion

        private bool CheckValidDepartmentId()
        {
            return true;
        }
    }
}
