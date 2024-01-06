using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class EditDepartmentViewModel: ViewModelCore
    {
		public EditDepartmentViewModel(Guid establishmentId, Department? department = null) {

			if (department == null)
			{
				EditedDepartment = MakeDepartment();
			}
			else
				EditedDepartment = department;

			this.employeeService = ServicesLocator.GetService<IEmployeeService>()!;
			this.departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;
			this.establishmentId = establishmentId;

			LoadViewModel();
		}


		private readonly IEmployeeService employeeService;
		private readonly IDepartmentsService departmentsService;

		private Guid establishmentId;
		private string departmentName;
		private Guid? selectedHeadOfLaboratory;
		ObservableCollection<Employee> employees = new();


		#region Properties
		public readonly Department EditedDepartment;

		public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnChanged(); } }
		public Guid SelectedHeadOfLaboratory { get => selectedHeadOfLaboratory; set {  selectedHeadOfLaboratory = value; OnChanged(); } }
		public string DepartmentName { get => departmentName; set { departmentName = value; OnChanged();} }
		#endregion

		#region MakeDepartment
		private Department MakeDepartment()
		{
			return new Department()
			{
				Id = default,
				Name = "Подразделение без названия",
				EstablishmentId = establishmentId, 
				HeadOfLabId = Guid.Empty,
			};
		}
		#endregion

		public void LoadViewModel()
		{
			this.DepartmentName = EditedDepartment.Name;
			this.SelectedHeadOfLaboratory = EditedDepartment.HeadOfLabId;

			LoadEmployees();
		}

		#region LoadEmployees
		public async void LoadEmployees()
		{
			var employees = await employeeService.GetEmployeesAsync(Guid.Empty, FetchAim.Index);
			employees.Insert(0, new Employee() { Id = Guid.Empty, FirstName = "Не задан", DepartmentId = Guid.Empty });

			Employees.Clear();
			Employees.AddRange(employees);

			if (EditedDepartment.HeadOfLabId == null || EditedDepartment.HeadOfLabId == Guid.Empty)
				selectedHeadOfLaboratory = Guid.Empty;
		}

		#endregion

		#region AcceptChanges
		public async Task<bool> AcceptChanges()
		{
			if (string.IsNullOrEmpty(departmentName))
			{
				MessageBox.Show("Название не может быть пустым", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			if (SelectedHeadOfLaboratory == null)
			{
				MessageBox.Show("Не выбран заведующий лабораторией", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}

			EditedDepartment.HeadOfLabId = SelectedHeadOfLaboratory;
			EditedDepartment.Name = departmentName;

			var existingDepartmentsIds = (await departmentsService.GetDepartmentsAsync(Guid.Empty)).Select(d => d.Id).ToList();
			if (existingDepartmentsIds.Contains(EditedDepartment.Id))
			{
				var updateResult = await departmentsService.UpdateDepartmentAsync(EditedDepartment);
				if (updateResult)
					MessageBox.Show("Сведения о подразделении были успешно обновлены");
			}
			else
			{
				var addResult = await departmentsService.AddDepartmentAsync(EditedDepartment);
				if (addResult)
					MessageBox.Show("Успешно добавлено новое подразделение");
			}
			return true;
		} 
		#endregion
	}
}
