using AG.Data;
using AG.Services.Utils;
using AG.Web.MVC.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AG.Web.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        #region ctor
        public EmployeeController(DataContext ctx)
        {
            _context = ctx;
        } 
        #endregion

        readonly DataContext _context;

        #region Index
        public async Task<IActionResult> Index(Guid departmentId)
        {
            var department = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == departmentId);
            if (department == null)
                return NotFound("Подразделение не найдено");

            var entities = await _context.EmplToDepTable
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.Employee)
                .Include(e => e.Function)
                .Where(e => e.DepartmentId == departmentId)
                .ToArrayAsync();

            var employees = entities.Select(t => new EmployeeVM()
            {
                Id = t.EmployeeId,
                Name = NameUtils.ToShortName(t.Employee!),
                Function = t.Function!.Name,
                Rate = t.Rate,
            }).ToList();

            var vm = new IndexEmployeeVM()
            {
                DepartmentName = department.Name,
                DepartmentId = departmentId,
                Employees = employees,
            };
            return View(vm);
        }
        #endregion
    }
}
