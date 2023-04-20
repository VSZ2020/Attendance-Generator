using Core.Database.AppEntities;
using Core.Database.Entities;
using Services.Database;
using Services.Extensions;
using Services.POCO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
                manager.Get<UserAccountEntity>(),
                manager.Get<FunctionEntity>(),
                manager.Get<EmployeeStatusEntity>());
            

            this.userAccountService = depsService;
            user = userAccountService.GetUserAccounts().First();

            LoadDepartments();
            LoadEmployees(SelectedDepartmentId);
        }

        public void LoadDepartments()
        {
            if (user == null)
                return;
            Departments.Clear();
            Departments.Add(new Department() { Id = 0, Name = "Все подразделения"});
            Departments.AddRange(userAccountService.GetAvailableDepartments(user));
           
            if (Departments.Count > 0)
                SelectedDepartmentId = 0;
        }

        public void LoadEmployees(int departmentId = 0)
        {
            if (user == null)
                return;
            Employees.Clear();
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
