using AG.Commands;
using AG.ViewModels.Forms;
using System;
using System.Windows;
using System.Windows.Input;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndDepartments.xaml
    /// </summary>
    public partial class WndDepartments : Window
    {
		#region ctor
		public WndDepartments(Guid establishmentId)
		{
			InitializeComponent();
			viewModel = new DepartmentsFormViewModel(establishmentId);
			DataContext = viewModel;
		}
		#endregion

		#region fields
		private readonly DepartmentsFormViewModel viewModel;
		#endregion

		#region CommandBinding_CanExecute
		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (e.Command == DepartmentsCommands.cmdAddDepartment) e.CanExecute = true;
			if (e.Command == DepartmentsCommands.cmdEditDepartment) e.CanExecute = viewModel?.SelectedDepartment != null;
			if (e.Command == DepartmentsCommands.cmdRemoveDepartment) e.CanExecute = viewModel?.Departments.Count > 0 && viewModel?.SelectedDepartment != null;
			if (e.Command == DepartmentsCommands.cmdShowEmployees) e.CanExecute = viewModel?.SelectedDepartment != null;
		} 
		#endregion

		#region CommandBinding_Executed
		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == DepartmentsCommands.cmdAddDepartment)
			{
				viewModel.AddDepartment();
				return;
			}

			if (e.Command == DepartmentsCommands.cmdEditDepartment)
			{
				viewModel.EditDepartment();
				return;
			}

			if (e.Command == DepartmentsCommands.cmdRemoveDepartment)
			{
				viewModel.RemoveDepartment();
				return;
			}

			if (e.Command == DepartmentsCommands.cmdShowEmployees)
			{
				viewModel.ShowEmployeesForm();
				return;
			}
		} 
		#endregion
	}
}
