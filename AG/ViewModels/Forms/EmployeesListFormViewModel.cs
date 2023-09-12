using AG.Windows;
using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AG.ViewModels.Forms
{
	public class EmployeesListFormViewModel : ViewModelCore
    {
		#region ctor
		public EmployeesListFormViewModel()
		{
			this.departmentsService = ServiceLocator.GetService<IDepartmentsService>()!;
			this.employesService = ServiceLocator.GetService<IEmployeeService>()!;
		}
		#endregion ctor

		#region fields
		private int selectedDepartmentId = 0;
        private readonly IDepartmentsService departmentsService;
        private readonly IEmployeeService employesService;
		private Employee selectedEmployee;
		#endregion fields

		#region Properties

		public ObservableCollection<Employee> Employees { get; set; } = new();

        public ObservableCollection<Department> Departments { get; set; } = new();

		public Employee SelectedEmployee { get=> selectedEmployee; set { selectedEmployee = value; OnChanged(); } }

        public int SelectedDepartmentId {  get => selectedDepartmentId; set  {  selectedDepartmentId = value;  OnChanged(nameof(SelectedDepartmentId));  } }
		#endregion properties

		public async Task InitializeWindow()
		{
			await LoadDepartmentsAsync();
			await LoadEmployeesAsync();
		}

		#region LoadDepartments
		public async Task LoadDepartmentsAsync()
        {
			ShowWaitMessage("Загрузка подразделений", "Подождите");
			var departmentsResult = await Task.Run(() => departmentsService.GetDepartments());
			ClearWaitMessage();

            Departments.Clear();

            if (departmentsResult.StatusCode == DatabaseResponse<Department>.ResponseCode.Success && departmentsResult.Results != null)
                Departments.AddRange(departmentsResult.Results);
            
            if (Departments.Count > 0)
                SelectedDepartmentId = 0;
		}
		#endregion

		#region LoadEmployees
		public async Task LoadEmployeesAsync()
        {
            if (SelectedDepartmentId < 0)
                return;
			ShowWaitMessage("Загрузка списка сотрудников", "Подождите");
			var employees = await Task.Run(() => employesService.GetEmployees(SelectedDepartmentId));
			ClearWaitMessage();

			Employees.Clear();
			if (employees.StatusCode == DatabaseResponse<Employee>.ResponseCode.Success && employees.Results != null)
				Employees.AddRange(employees.Results);
		}
		#endregion

		#region ShowEmployeeTimeIntervals()
		public void ShowEmployeeTimeIntervals()
		{
			new WndEmployeeTimeIntervals(SelectedEmployee).ShowDialog();
			//MessageBox.Show("Не выбран сотрудник для отображения неявок", "", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
		#endregion
	}
}
