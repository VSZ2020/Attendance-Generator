using AG.Windows;
using AG.WPF.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AG.WPF.ViewModels.Forms
{
    public class EmployeesListFormViewModel : ViewModelCore
    {
        #region ctor
        public EmployeesListFormViewModel(Guid? filterDepartmentId = null)
        {
            departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;
            employesService = ServicesLocator.GetService<IEmployeeService>()!;

            fixedDepartmentFilterId = filterDepartmentId;

            InitializeWindow();
        }
        #endregion ctor

        #region fields
        private readonly IDepartmentsService departmentsService;
        private readonly IEmployeeService employesService;
        private Guid selectedDepartmentId = Guid.Empty;
        private Employee selectedEmployee;
        private readonly Guid? fixedDepartmentFilterId;
        #endregion fields

        #region Properties

        public ObservableCollection<Employee> Employees { get; set; } = new();

        public ObservableCollection<Department> Departments { get; set; } = new();

        public Employee SelectedEmployee { get => selectedEmployee; set { selectedEmployee = value; OnChanged(); } }

        public Guid SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(nameof(SelectedDepartmentId)); } }
        #endregion properties

        #region InitializeWindow
        public async Task InitializeWindow()
        {
            await LoadDepartmentsAsync();
            await LoadEmployeesAsync();
        }
        #endregion

        #region LoadDepartments
        public async Task LoadDepartmentsAsync()
        {
            ShowWaitMessage("Загрузка подразделений", "Подождите");

            if (fixedDepartmentFilterId != null)
            {
                var department = await Task.Run(() => departmentsService.GetDepartmentByIdAsync(fixedDepartmentFilterId.Value));
                Departments.Clear();
                Departments.Add(department);
            }
            else
            {
                var departmentsResult = await Task.Run(() => departmentsService.GetDepartmentsAsync(Guid.Empty));

                Departments.Clear();
                Departments.Add(new Department() { Id = Guid.Empty, Name = "Все подразделения" });
                Departments.AddRange(departmentsResult);
            }

            SelectedDepartmentId = Departments.Count > 0 ? Departments.First().Id : Guid.Empty;

            ClearWaitMessage();
        }
        #endregion

        #region LoadEmployeesAsync
        public async Task LoadEmployeesAsync()
        {
            ShowWaitMessage("Загрузка списка сотрудников", "Подождите");

            var employees = await employesService.GetEmployeesAsync(SelectedDepartmentId, FetchAim.Table);

            Employees.Clear();
            Employees.AddRange(employees);

            ClearWaitMessage();
        }
        #endregion

        #region ShowEmployeeTimeIntervals()
        public void ShowEmployeeTimeIntervals()
        {
            new WndEmployeeTimeIntervals(SelectedEmployee).ShowDialog();
            //MessageBox.Show("Не выбран сотрудник для отображения неявок", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region AddEmployee
        public async void AddEmployee()
        {
            new WndEditEmployee().ShowDialog();
            await LoadEmployeesAsync();
        }
        #endregion

        #region ShowEditEmployeeForm
        public async void EditEmployee()
        {
            if (selectedEmployee != null)
            {
                new WndEditEmployee(SelectedEmployee).ShowDialog();
                await LoadEmployeesAsync();
            }
            else
                MessageBox.Show("Для редактирования сотрудника, сначала выберите его из списка");
        }
        #endregion

        #region RemoveEmployee
        public async void RemoveEmployee()
        {
            if (selectedEmployee != null)
            {
                try
                {
                    var removeStatus = await employesService.DeleteEmployeeAsync(SelectedEmployee.Id);
                    if (removeStatus)
                        Employees.Remove(SelectedEmployee);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Не удалось удалить сотрудника. {ex.Message}", "Внимание", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить сотрудника. {ex.Message}", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        #endregion
    }
}
