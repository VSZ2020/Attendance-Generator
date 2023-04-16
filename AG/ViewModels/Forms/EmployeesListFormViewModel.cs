using Core.Database.AppEntities;
using Core.Database.Entities;
using Services.Database;
using Services.Extensions;
using Services.POCO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AG.ViewModels.Forms
{
    public class EmployeesListFormViewModel : INotifyPropertyChanged
    {
        private int selectedDepartmentId = 0;
        private string selectedDepartmentIdText;
        private readonly UserAccountService userAccountService;

        public UserAccount? user;
        public ObservableCollection<Employee> Employees { get; set; } = new();

        public ObservableCollection<Department> Departments { get; set; } = new();

        public int SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(nameof(SelectedDepartmentIdText)); } }
        public string SelectedDepartmentIdText { get => selectedDepartmentId.ToString(); set { selectedDepartmentIdText = value; OnChanged(); } }

        public EmployeesListFormViewModel()
        {
            ProvidersManager manager = new ProvidersManager();
            UserAccountService depsService = new UserAccountService(
                manager.Get<DepartmentEntity>(), 
                manager.Get<EmployeeEntity>(), 
                manager.Get<UserAccountEntity>());
            

            this.userAccountService = depsService;

            LoadDepartments();
        }

        private void LoadDepartments()
        {
            if (user == null)
                return;
            Departments.AddRange(userAccountService.GetAvailableDepartments(user));
            if (Departments.Count > 0)
                SelectedDepartmentId = 1;
        }

        private void LoadEmployees(int departmentId = 0)
        {
            if (user == null)
                return;
            Employees.AddRange(userAccountService.GetAvailableEmployees(user, departmentId));
        }

        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnChanged([CallerMemberName]string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
