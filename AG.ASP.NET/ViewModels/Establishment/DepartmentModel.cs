using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Database;
using Services.Domains;

namespace AG.ASP.NET.ViewModels.Establishment
{
    public class DepartmentModel
    {
        public DepartmentModel(Guid departmentId, IDepartmentsService depService, IEmployeeService emplService)
        {
            this.depService = depService;
            this.empService = emplService;
            this.departmentId = departmentId;

            Init();
        }
        private readonly Guid departmentId;

        private readonly IDepartmentsService depService;
        private readonly IEmployeeService empService;

        public string DepartmentName { get; set; }

        public string HeadOfLab { get; set; }

        public List<Employee> AvailableEmployees { get; set; } = new List<Employee>();

        
        public async void Init()
        {
            await LoadAvailableHeaderOfLabAsync();
            await LoadDepartmentNameAsync();
        }

        public async Task LoadAvailableHeaderOfLabAsync()
        {
            var employees = await empService.GetEmployeesAsync(Guid.Empty, FetchAim.Table, FetchAim.None, FetchAim.None, FetchAim.Index);
            
            if (employees != null)
            {
                AvailableEmployees.Clear();
                AvailableEmployees.AddRange(employees);
            }
            

        }

        public async Task LoadDepartmentNameAsync()
        {
            var currentDepartment = await depService.GetDepartmentByIdAsync(departmentId, FetchAim.Index);
            this.DepartmentName = currentDepartment.Name;
        }

    }
}
