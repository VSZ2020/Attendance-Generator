using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Extensions;
using Services.POCO;
using SQLiteRepository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AG.ViewModels.Forms
{
    public class EmployeesListFormViewModel : INotifyPropertyChanged
    {
        private int selectedDepartmentId = 0;
        private string selectedDepartmentIdText;
        private readonly DepartmentsService departmentsService;
        private readonly EmployeesService employesService;

        public UserAccount? user;
        public ObservableCollection<Employee> Employees { get; set; } = new();

        public ObservableCollection<Department> Departments { get; set; } = new();

        public int SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(nameof(SelectedDepartmentIdText)); } }
        public string SelectedDepartmentIdText { get => selectedDepartmentId.ToString(); set { selectedDepartmentIdText = value; OnChanged(); } }

        public EmployeesListFormViewModel()
        {
            var provider = ServiceLocator.Services.BuildServiceProvider();
            this.departmentsService = new DepartmentsService(provider.GetService<IEstablishmentItemsRepository>());
            this.employesService = new EmployeesService(provider.GetService<IEstablishmentItemsRepository>());
            user = SessionService.User;

            LoadDepartments();
            if (Departments.Count > 1) LoadEmployees(SelectedDepartmentId);
        }

        public void LoadDepartments()
        {
            if (user == null)
                return;
            var departments = departmentsService.GetAvailableDepartments(user);

            Departments.Clear();
            Departments.Add(new Department() { Id = 0, Name = "Все подразделения"});
            if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.Success)
                Departments.AddRange(departments.Results);
            else if (departments.StatusCode == DatabaseResponse<Department>.ResponseCode.PermissionsError && !string.IsNullOrEmpty(departments.Message))
                ShowMessage(departments.Message);
           
            if (Departments.Count > 0)
                SelectedDepartmentId = 0;
        }

        public void LoadEmployees(int departmentId = 0)
        {
            if (user == null)
                return;
            var employees = employesService.GetAvailableEmployees(user, departmentId);
            Employees.Clear();
            if (employees.StatusCode == DatabaseResponse<Employee>.ResponseCode.Success) 
                Employees.AddRange(employees.Results);
            else if (employees.StatusCode == DatabaseResponse<Employee>.ResponseCode.PermissionsError && !string.IsNullOrEmpty(employees.Message))
                ShowMessage(employees.Message);
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
