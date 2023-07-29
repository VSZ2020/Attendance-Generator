using AG.Services;
using AG.Windows;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class EmployeesListFormViewModel : INotifyPropertyChanged
    {
		#region ctor
		public EmployeesListFormViewModel()
		{
			this.departmentsService = ServiceLocator.Provider.GetService<IDepartmentsService>()!;
			this.employesService = ServiceLocator.Provider.GetService<IEmployeeService>()!;
			user = SessionService.User;

			
		}
		#endregion ctor

		#region fields
		private int selectedDepartmentId = 0;
        private readonly IDepartmentsService departmentsService;
        private readonly IEmployeeService employesService;
		private UserAccount? user;
		#endregion fields

		#region properties

		public ObservableCollection<Employee> Employees { get; set; } = new();

        public ObservableCollection<Department> Departments { get; set; } = new();

        public int SelectedDepartmentId { 
            get => selectedDepartmentId; 
            set 
            { 
                selectedDepartmentId = value; 
                OnChanged(nameof(SelectedDepartmentIdText)); 
            } 
        }
        public string SelectedDepartmentIdText { 
            get => selectedDepartmentId.ToString(); 
            set 
            { 
                OnChanged(); 
            } 
        }
		#endregion properties


		public async Task LoadDepartments()
        {
            if (user == null)
                return;

            var departmentsTask = departmentsService.GetDepartmentsAsync(user);
            //Отображение для пользователя окна ожидания
            var wndWait = new WndWait();
            wndWait.Show();
            var departments = await departmentsTask;

            Departments.Clear();
            Departments.Add(new Department() { 
                Id = 0, 
                Name = "Все подразделения"
            });

            if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.Success && departments.Results != null)
                Departments.AddRange(departments.Results);
            
            if (Departments.Count > 0)
                SelectedDepartmentId = 0;

			wndWait.Close();
		}

        public void LoadEmployees(int departmentId = 0)
        {
            if (user == null)
                return;

            var employees = employesService.GetEmployees(departmentId);

            Employees.Clear();
            if (employees.StatusCode == DatabaseResponse<Employee>.ResponseCode.Success && employees.Results != null) 
                Employees.AddRange(employees.Results);
        }

        #region Message
        private void ShowMessage(string msg, string? header = null)
        {
            MessageBox.Show(msg, header ?? "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnChanged([CallerMemberName]string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
