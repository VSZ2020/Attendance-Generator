using AG.WPF.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using Services.Session;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace AG.WPF.ViewModels.Forms
{
    public class EditEmployeeViewModel : ViewModelCore
    {
        #region ctor
        public EditEmployeeViewModel(Employee? employee = null)
        {
            editedEmployee = employee == null ? MakeEmployee() : employee;

            employeeService = ServicesLocator.GetService<IEmployeeService>()!;
            departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;

            InitializeViewModel();
        }
        #endregion

        #region fields
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentsService departmentsService;

        private Employee editedEmployee;

        private string firstName;
        private string lastName;
        private string middleName;
        private string email;
        private string phone;

        private Guid selectedFunctionId;
        private Guid selectedStatusId;
        private Guid selectedDepartmentId;

        private float rate;
        private bool isConcurrent;

        private ObservableCollection<Department> departmentsList = new();
        private ObservableCollection<EmployeeStatus> statusesList = new();
        private ObservableCollection<EmployeeFunction> functionsList = new();
        #endregion

        #region Properties
        public string FirstName { get => firstName; set { firstName = value; OnChanged(); } }
        public string MiddleName { get => middleName; set { middleName = value; OnChanged(); } }
        public string LastName { get => lastName; set { lastName = value; OnChanged(); } }

        public float Rate { get => rate; set { rate = value; OnChanged(); } }
        public bool IsConcurrent { get => isConcurrent; set { isConcurrent = value; OnChanged(); } }


        public Guid SelectedDepartmentId { get => selectedDepartmentId; set { selectedDepartmentId = value; OnChanged(); } }
        public Guid SelectedFunctionId { get => selectedFunctionId; set { selectedFunctionId = value; OnChanged(); } }
        public Guid SelectedStatusId { get => selectedStatusId; set { selectedStatusId = value; OnChanged(); } }


        public string Phone { get => phone; set { phone = value; OnChanged(); } }
        public string Email { get => email; set { email = value; OnChanged(); } }

        public string ShortName => string.Join("", LastName, " ", FirstName[0], ". ", MiddleName[0], ".");
        public string FullName => string.Join(" ", LastName, FirstName, MiddleName);

        public ObservableCollection<EmployeeStatus> StatusesList { get => statusesList; set { statusesList = value; OnChanged(); } }
        public ObservableCollection<EmployeeFunction> FunctionsList { get => functionsList; set { functionsList = value; OnChanged(); } }
        public ObservableCollection<Department> DepartmentsList { get => departmentsList; set { departmentsList = value; OnChanged(); } }
        #endregion

        #region InitializeViewModel
        public async void InitializeViewModel()
        {
            var estId = SessionService.CurrentEstablishemntId;
            var departments = estId.HasValue ? await departmentsService.GetDepartmentsAsync(estId.Value, FetchAim.Index) : new List<Department>();
            var statuses = await employeeService.GetStatusesAsync(FetchAim.Index);

            StatusesList.Clear();
            DepartmentsList.Clear();
            FunctionsList.Clear();

            StatusesList.AddRange(statuses);
            DepartmentsList.AddRange(departments);

            var functionGroups = await employeeService.GetFunctionGroupsAsync();
            foreach (var group in functionGroups)
            {
                var functions = await employeeService.GetFunctionsAsync(group.Id);
                FunctionsList.AddRange(functions);
            }

            FillViewFields();
        }
        #endregion

        #region MakeEmployee
        public Employee MakeEmployee()
        {
            return new Employee() { Id = Guid.NewGuid() };
        }
        #endregion

        #region ValidateViewModel
        public bool ValidateViewModel()
        {
            ClearValidationMessages();

            if (string.IsNullOrEmpty(firstName))
                AddValidationMessage("Введите имя сотрудника", "Имя");

            if (string.IsNullOrEmpty(lastName))
                AddValidationMessage("Заполните фамилию сотрудника", "Фамилия");

            if (rate <= 0)
                AddValidationMessage("Доля ставки не может быть меньше или равной 0", "Доля ставки");

            if (!string.IsNullOrEmpty(phone) && !Regex.IsMatch(phone, "/+7 /(/d/d/d)/ /d/d/d-/d/d-/d/d"))
                AddValidationMessage("Номер телефона должен быть в формате '+7 (000) 000-00-00'", "Номер телефона");

            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, "(/w)+@/w+/.(/./w+)*"))
                AddValidationMessage("Адрес электронной почты имеет неверный формат", "E-mail");

            if (selectedDepartmentId == Guid.Empty)
                AddValidationMessage("Выберите подразделение сотрудника", "Подразделение");

            if (selectedStatusId == Guid.Empty)
                AddValidationMessage("Выберите статус сотрудника", "Статус");

            if (selectedFunctionId == Guid.Empty)
                AddValidationMessage("Не задана должность сотрудника", "Должность");

            return IsValid;
        }
        #endregion

        #region FillViewFields
        public void FillViewFields()
        {
            FirstName = editedEmployee.FirstName;
            LastName = editedEmployee.LastName;
            MiddleName = editedEmployee.MiddleName;
            Email = editedEmployee.Email;
            Phone = editedEmployee.PhoneNumber;
            Rate = editedEmployee.Rate;
            IsConcurrent = editedEmployee.IsConcurrent;

            SelectedDepartmentId = editedEmployee.DepartmentId != Guid.Empty ? editedEmployee.DepartmentId : departmentsList.FirstOrDefault()?.Id ?? Guid.Empty;
            SelectedFunctionId = editedEmployee.FunctionId != Guid.Empty ? editedEmployee.FunctionId : functionsList.FirstOrDefault()?.Id ?? Guid.Empty;
            SelectedStatusId = editedEmployee.StatusId != Guid.Empty ? editedEmployee.StatusId : statusesList.FirstOrDefault()?.Id ?? Guid.Empty;
        }
        #endregion

        #region AcceptChanges
        public async Task<bool> AcceptChanges()
        {
            ShowWaitMessage("Выполняется сохранение", "Пожалуйста, подождите");

            if (!ValidateViewModel()) return false;

            editedEmployee.FirstName = firstName;
            editedEmployee.LastName = lastName;
            editedEmployee.MiddleName = middleName;
            editedEmployee.Email = email;
            editedEmployee.PhoneNumber = phone;
            editedEmployee.Rate = rate;
            editedEmployee.IsConcurrent = isConcurrent;

            editedEmployee.StatusId = selectedStatusId;
            editedEmployee.DepartmentId = selectedDepartmentId;
            editedEmployee.FunctionId = selectedFunctionId;

            var saveResult = false;
            try
            {
                var existingEmployeesIds = (await employeeService.GetEmployeesAsync(Guid.Empty)).Select(e => e.Id).ToImmutableList();

                if (!existingEmployeesIds.Contains(editedEmployee.Id))
                    saveResult = await employeeService.AddEmployeeAsync(editedEmployee);
                else
                    saveResult = await employeeService.UpdateEmployeeAsync(editedEmployee);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, "Employee editor");
                MessageBox.Show($"Не удалось сохранить сотрудника из-за непредвиденной ошибки: {ex.Message}");
                return false;
            }
            finally
            {
                ClearWaitMessage();
            }
            return saveResult;
        }
        #endregion
    }
}
