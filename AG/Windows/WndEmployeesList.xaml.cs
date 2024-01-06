using AG.Commands;
using AG.ViewModels.Forms;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEmployeesList.xaml
    /// </summary>
    public partial class WndEmployeesList : Window
    {
		#region ctor
		public WndEmployeesList(Guid? FilterByDepartmentId = null)
        {
            InitializeComponent();

            viewModel = new EmployeesListFormViewModel(FilterByDepartmentId);
			DataContext = viewModel;

			this.Loaded += WndEmployeesList_Loaded;
        }
		#endregion

		#region fields
		private readonly EmployeesListFormViewModel? viewModel;
		#endregion

		#region Window Loaded
		private async void WndEmployeesList_Loaded(object sender, RoutedEventArgs e)
		{
			cbDepartment.SelectionChanged += cbDepartment_SelectionChanged;
		}
		#endregion

		#region ComboBox Department SelectionChanged
		private async void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			await viewModel!.LoadEmployeesAsync();
		}
		#endregion

		private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
		{
			if (e.Command.Equals(EmployeesListCommands.cmdTimeIntervals))
				e.CanExecute = viewModel?.SelectedEmployee != null;

			if (e.Command == EmployeesListCommands.cmdAddEmployee)
				e.CanExecute = viewModel != null;

			if (e.Command == EmployeesListCommands.cmdEditEmployee)
				e.CanExecute = viewModel?.Employees.Count > 0;

			if (e.Command == EmployeesListCommands.cmdRemoveEmployee)
				e.CanExecute = viewModel?.Employees.Count > 0;
		}

		private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			if (e.Command == EmployeesListCommands.cmdTimeIntervals)
			{
				viewModel?.ShowEmployeeTimeIntervals();
			}

			if (e.Command == EmployeesListCommands.cmdAddEmployee)
			{
				viewModel?.AddEmployee();
			}

			if (e.Command == EmployeesListCommands.cmdEditEmployee)
			{
				viewModel?.EditEmployee();
			}

			if (e.Command == EmployeesListCommands.cmdRemoveEmployee)
			{
				viewModel?.RemoveEmployee();
			}
		}

		private void employeeGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			viewModel?.EditEmployee();
		}
	}
}
