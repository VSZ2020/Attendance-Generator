using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Database;
using Services.Domains;
using Services.Domains.Security;

namespace AG.Web.Pages.Establishment
{
    public class EmployeesModel : PageModel
    {
        public EmployeesModel([FromServices]IEmployeeService emplService, [FromServices]IDepartmentsService depService)
        {
            this.emplService = emplService;
            this.depService = depService;
        }

        private readonly IEmployeeService emplService;
        private readonly IDepartmentsService depService;

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = "";

        public IList<Employee> EmployeesList { get; set; } = new List<Employee>();

        public IActionResult OnGet(Guid departmentId)
        {
            if (departmentId == Guid.Empty && (!HttpContext.User.IsInRole(CustomRoles.ADMIN) && !HttpContext.User.IsInRole(CustomRoles.MODERATOR)))
            {
                return Unauthorized();
            }

            PopulateTable(departmentId);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return NotFound("Сотрудник с таким идентификатором не найден");
            }
            await emplService.DeleteEmployeeAsync(employeeId);
            return RedirectToPage();
        }

        public async void PopulateTable(Guid departmentId)
        {
            this.DepartmentId = departmentId;
            var department = await depService.GetDepartmentByIdAsync(departmentId);
            this.DepartmentName = department?.Name ?? "";

            var employees = await emplService.GetEmployeesAsync(departmentId, FetchAim.Table);
            if (employees != null)
            {
                this.EmployeesList = employees;
            }
        }
    }
}
