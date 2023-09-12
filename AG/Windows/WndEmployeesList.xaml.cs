using AG.Commands;
using AG.ViewModels.Forms;
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
		public WndEmployeesList()
        {
            InitializeComponent();

            viewModel = (EmployeesListFormViewModel)this.Resources["viewModel"];
			this.Loaded += WndEmployeesList_Loaded;
        }
		#endregion

		#region fields
		private readonly EmployeesListFormViewModel? viewModel;
		#endregion

		#region Window Loaded
		private async void WndEmployeesList_Loaded(object sender, RoutedEventArgs e)
		{
			await viewModel!.InitializeWindow();
			if (viewModel?.Departments.Count > 1) cbDepartment.SelectedIndex = 0;
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
			
		}

		private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
		{
			if (e.Command == EmployeesListCommands.cmdTimeIntervals)
			{
				viewModel?.ShowEmployeeTimeIntervals();
			}
		}
	}
}
