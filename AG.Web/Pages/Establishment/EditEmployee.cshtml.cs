using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Database;
using Services.Domains;

namespace AG.Web.Pages.Establishment
{
    public class EditEmployeeModel : PageModel
    {
        #region Fields
        private Guid departmentId = Guid.Empty;

        private readonly IEmployeeService emplService;
        private readonly IDepartmentsService depService;
        #endregion

        #region Properties
        public string EmployeeFullName { get; set; }

        [BindProperty(SupportsGet = true)]
        public Employee CurrentEmployee { get; set; }

        public Guid SelectedEmployeeDepartmentId { get; set; }
        public Guid SelectedEmployeeStatusId { get; set; }
        public Guid SelectedEmployeeFunctionId { get; set; }

        public SelectList AvailableFunctionsSL { get; set; }
        public SelectList AvailableDepartmentsSL { get; set; }
        public SelectList AvailableStatusesSL { get; set; }
        #endregion

        public EditEmployeeModel([FromServices]IEmployeeService emplService, [FromServices] IDepartmentsService depService)
        {
            this.emplService = emplService;
            this.depService = depService;
        }

        public IActionResult OnGet(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                return NotFound();

            Init(employeeId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid employeeId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = await emplService.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
                return NotFound();

            var modelUpdateResult = await TryUpdateModelAsync<Employee>(employee, "CurrentEmployee",
                e => e.LastName, e => e.FirstName, e => e.MiddleName, 
                e => e.Email, e => e.PhoneNumber, 
                e => e.IsConcurrent, e => e.Rate);

            if (modelUpdateResult)
            {
                var result = await emplService.UpdateEmployeeAsync(employee);
            }

            Init(employeeId);
            return RedirectToPage("Employees");
        }

        public async void Init(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                this.CurrentEmployee = new Employee() { DepartmentId = this.departmentId,FunctionId = Guid.Empty, StatusId = Guid.Empty };
            }
            else
            {
                //Load current employee
                var employee = await emplService.GetEmployeeByIdAsync(employeeId, FetchAim.Card);
                this.CurrentEmployee = employee;
                this.departmentId = employee.DepartmentId;
            }

            //Заполняем перечень доступных подразделений
            var curEstablishmentId = (await depService.GetDepartmentByIdAsync(this.departmentId)).EstablishmentId;
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
            }

            //Заполняем имя ФИО сотрудника
            this.EmployeeFullName = this.CurrentEmployee.FullName;
        }
    }
}
