using AG.Windows;
using Core.ViewModel;
using Services.Database;
using Services.Domains;
using Services.Extensions;
using Services.Infrastructure;
using Services.Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class DepartmentsFormViewModel: ViewModelCore
    {
		#region ctor
		public DepartmentsFormViewModel(Guid establishmentId)
		{
			this.departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;
			this.employeeService = ServicesLocator.GetService<IEmployeeService>()!;

			this.establishmentId = establishmentId;
			//this.principal = Thread.CurrentPrincipal;

			LoadDepartments();
		}
		#endregion

		#region fields
		private readonly IDepartmentsService departmentsService;
		private readonly IEmployeeService employeeService;

		private readonly Guid establishmentId;
        private Department? selectedDepartment;

		//private readonly IPrincipal principal;
		#endregion

		#region Properties
		public ObservableCollection<Department> Departments { get; set; } = new();

        public Department? SelectedDepartment { get => selectedDepartment; set { selectedDepartment = value; OnChanged(); } }
		#endregion

		#region LoadDepartments
		public async Task LoadDepartments()
		{
			base.ShowWaitMessage("Загрузка списка подразделений", "Пожалуйста, подождите");

			var departments = await Task.Run(() => departmentsService.GetDepartmentsAsync(establishmentId, FetchAim.Table));

			//Получаем информацию о количестве сотрудников
			await LoadDepartmentsEmployeesCountAsync(departments);

			Departments.Clear();
			Departments.AddRange(departments);

			base.ClearValidationMessages();
		}
		#endregion

		#region LoadDepartmentsEmployeesCountAsync
		private async Task LoadDepartmentsEmployeesCountAsync(IList<Department> departments)
		{
			foreach (var dep in departments)
			{
				dep.EmployeesCount = await employeeService.GetEmployeesCountAsync(dep.Id);
			}
		} 
		#endregion

		#region AddDepartment
		public async void AddDepartment()
		{
			//if (principal == null || !principal.IsInRole(RolesDefault.ADMINISTRATOR) || !principal.IsInRole(RolesDefault.MODERATOR))
			//{
			//	MessageBox.Show("У Вас нет прав на создание подразделения. Обратитесь к администратору!", "Ошибка доступа",MessageBoxButton.OK, MessageBoxImage.Hand);
			//	return;
			//}

			try
			{
				new WndEditDepartment(establishmentId).ShowDialog();
			}
			catch (UnauthorizedAccessException uex)
			{
				Logger.Log(uex, "Add department");
				MessageBox.Show(uex.Message);
			}
			catch (Exception ex)
			{
				Logger.Log(ex, "Add department");
				MessageBox.Show(ex.Message);
			}
		} 
		#endregion

		#region EditDepartment
		public async void EditDepartment()
		{
			if (SelectedDepartment != null)
			{
				//var user = principal.Identity as UserIdentity;
				//var isUserDepartment = (user?.UserDepartmentId ?? Guid.Empty) == SelectedDepartment.Id;
				//if (principal == null || ((!principal.IsInRole(RolesDefault.ADMINISTRATOR) || !principal.IsInRole(RolesDefault.MODERATOR)) && !isUserDepartment))
				//{
				//	MessageBox.Show("У Вас нет прав на редактирование подразделения. Обратитесь к администратору!", "Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Hand);
				//	return;
				//}

				try
				{
					new WndEditDepartment(establishmentId, SelectedDepartment).ShowDialog();					
				}
				catch (UnauthorizedAccessException uex)
				{
					Logger.Log(uex, "Update department");
					MessageBox.Show(uex.Message);
				}
				catch (Exception ex)
				{
					Logger.Log(ex, "Update department");
					MessageBox.Show(ex.Message);
				}
			}
		} 
		#endregion

		#region RemoveDepartment
		public async void RemoveDepartment()
		{
			if (SelectedDepartment != null)
			{
				//if (principal == null || !principal.IsInRole(RolesDefault.ADMINISTRATOR) || !principal.IsInRole(RolesDefault.MODERATOR))
				//{
				//	MessageBox.Show("У Вас нет прав на удаление подразделения. Обратитесь к администратору или модератору!", "Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Hand);
				//	return;
				//}

				try
				{
					var isSuccessRemove = await departmentsService.RemoveDepartmentAsync(SelectedDepartment.Id);

					if (isSuccessRemove)
					{
						Departments.Remove(SelectedDepartment);
						MessageBox.Show("Успешно удалено!");
					}
				}
				catch (UnauthorizedAccessException uex)
				{
					Logger.Log(uex, "Remove department");
					MessageBox.Show(uex.Message);
				}
				catch (Exception ex)
				{
					Logger.Log(ex, "Remove department");
					MessageBox.Show(ex.Message);
				}
			}
			else
				MessageBox.Show("Выберите подразделение для удаления");
		}
		#endregion

		#region ShowEmployeesForm
		public void ShowEmployeesForm()
		{
			new WndEmployeesList(selectedDepartment!.Id).ShowDialog();
		} 
		#endregion
	}
}
