using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Database;
using Services.Domains;

namespace AG.Web.Pages.Establishment
{
    public class CreateEmployeeModel : PageModel
    {
        #region Fields
        private readonly IEmployeeService emplService;
        private readonly IDepartmentsService depService;
        #endregion

        #region Properties
        public string EmployeeFullName { get; set; }

        [BindProperty(SupportsGet = true)]
        public Employee CurrentEmployee { get; set; } = new();

        public Guid SelectedEmployeeDepartmentId { get; set; }
        public Guid SelectedEmployeeStatusId { get; set; }
        public Guid SelectedEmployeeFunctionId { get; set; }

        public SelectList AvailableFunctionsSL { get; set; }
        public SelectList AvailableDepartmentsSL { get; set; }
        public SelectList AvailableStatusesSL { get; set; }
        #endregion

        public CreateEmployeeModel([FromServices]IEmployeeService emplService, [FromServices] IDepartmentsService depService)
        {
            this.emplService = emplService;
            this.depService = depService;
        }

        public IActionResult OnGet(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                return NotFound();
            else
                this.CurrentEmployee.DepartmentId = departmentId;

            Init();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool isConcurrent)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = new Employee();
            var modelUpdateResult = await TryUpdateModelAsync<Employee>(employee, "CurrentEmployee",
                e => e.LastName, e => e.FirstName, e => e.MiddleName, 
                e => e.Email, e => e.PhoneNumber, 
                e => e.IsConcurrent, e => e.Rate, 
                e => e.DepartmentId, e => e.FunctionId, e => e.StatusId);

            if (modelUpdateResult)
            {
                var result = await emplService.AddEmployeeAsync(employee);
                return RedirectToPage("Employees");
            }

            Init();
            return Page();
        }

        public async void Init()
        {
            //Заполняем перечень доступных подразделений
            var curEstablishmentId = (await depService.GetDepartmentByIdAsync(this.CurrentEmployee.DepartmentId)).EstablishmentId;
            var departments = await depService.GetDepartmentsAsync(curEstablishmentId, FetchAim.Index);
            if (departments != null)
            {
                this.AvailableDepartmentsSL = new SelectList(departments, nameof(Department.Id), nameof(Department.Name), SelectedEmployeeDepartmentId);
            }

            //Заполняем перечень доступных должностей
            var functions = await emplService.GetFunctionsAsync(Guid.Empty, FetchAim.Index);
            if (functions != null)
            {
                this.AvailableFunctionsSL = new SelectList(functions, nameof(EmployeeFunction.Id), nameof(EmployeeFunction.Name), SelectedEmployeeFunctionId);
            }

            //Заполняем перечень доступных статусов
            var statuses = await emplService.GetStatusesAsync();
            if (statuses != null)
            {
                this.AvailableStatusesSL = new SelectList(statuses, nameof(EmployeeStatus.Id), nameof(EmployeeStatus.Name), SelectedEmployeeStatusId);
                //this.CurrentEmployee.StatusId = statuses.First().Id;
            }

            //Заполняем имя ФИО сотрудника
            this.EmployeeFullName = this.CurrentEmployee.FullName;
        }
    }
}
