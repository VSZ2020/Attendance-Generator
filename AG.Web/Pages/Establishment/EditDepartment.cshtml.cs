using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Database;
using Services.Domains;

namespace AG.Web.Pages.Establishment
{
    public class EditDepartmentModel : PageModel
    {
        public EditDepartmentModel([FromServices]IDepartmentsService depService, [FromServices]IEmployeeService emplService)
        {
            this.depService = depService;
            this.empService = emplService;
        }

        private readonly IDepartmentsService depService;
        private readonly IEmployeeService empService;

        [BindProperty(SupportsGet = true)]
        public Department CurrentDepartment { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DepartmentName { get; set; }

        public Guid SelectedHeadOfLabId { get; set; }

        public SelectList AvailableEmployees { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                return NotFound();

            var department = await depService.GetDepartmentByIdAsync(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            PopulateList(department);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                return NotFound();

            CurrentDepartment = await depService.GetDepartmentByIdAsync(departmentId, FetchAim.Card);
            if (CurrentDepartment == null)
            {
                return NotFound();
            }

            var modelUpdateResult = await TryUpdateModelAsync<Department>(CurrentDepartment, "CurrentDepartment",
                e => e.Name, e => e.Id, e => e.HeadOfLabId);

            if (modelUpdateResult)
            {
                await depService.UpdateDepartmentAsync(CurrentDepartment);
                return Redirect("Establishment/Departments");
            }

            PopulateList(CurrentDepartment);
            return Page();
        }

        public async void PopulateList(Department department)
        {
            var employees = await empService.GetEmployeesAsync(department.Id, FetchAim.Index);
            this.DepartmentName = department.Name;

            AvailableEmployees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.FullName), SelectedHeadOfLabId);
        } 

    }
}
