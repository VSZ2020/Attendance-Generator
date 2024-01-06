using AG.Commands;
using AG.ViewModels.Forms;
using AG.Windows;
using Services;
using Services.Database;
using Services.Domains;
using System;
using System.Windows;
using System.Windows.Input;

namespace AG
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
		#region ctor
		public MainWindow()
		{
			InitializeComponent();
			viewModel = new MainWindowViewModel();
			DataContext = viewModel;
		}
		#endregion

		#region fields
		private readonly MainWindowViewModel viewModel;
		#endregion fields

		#region CommandBinding_CanExecute
		private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (e.Command.Equals(MainUICommands.cmdOpenFile))
				e.CanExecute = viewModel.IsLoggedIn;

			if (e.Command.Equals(MainUICommands.cmdOrganizationInfo))
				e.CanExecute = viewModel.IsLoggedIn;
			if (e.Command.Equals(MainUICommands.cmdDepartmentsList))
				e.CanExecute = viewModel.IsLoggedIn;
			if (e.Command.Equals(MainUICommands.cmdOpenEmployeesList))
				e.CanExecute = viewModel.IsLoggedIn;

			if (e.Command.Equals(MainUICommands.cmdViewSheet))
				e.CanExecute = viewModel.IsLoggedIn;

			if (e.Command.Equals(MainUICommands.cmdGenerateSheet))
				e.CanExecute = viewModel.IsLoggedIn;

			if (e.Command.Equals(MainUICommands.cmdOrganizationInfo))
				e.CanExecute = viewModel.IsLoggedIn && viewModel.EstablishmentId != Guid.Empty;
		} 
		#endregion

		#region CommandBinding_Executed
		private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == MainUICommands.cmdOpenEmployeesList)
			{
				viewModel.ShowEmployeesList();
				return;
			}
			if (e.Command == MainUICommands.cmdDepartmentsList)
			{
				viewModel.ShowDepartmentsForm();
				return;
			}

			if (e.Command == MainUICommands.cmdViewSheet)
			{
				viewModel.ShowReportForm();
				return;
			}

			if (e.Command == MainUICommands.cmdOrganizationInfo)
			{
				viewModel.ShowOrganizationForm();
				return;
			}
		}
		#endregion

		#region MenuItem_Click
		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (sender == miLogin)
			{
				viewModel.LoginUser();
				return;
			}
			if (sender == miLogout)
			{
				viewModel.LogoutUser(); return;
			}
		
		} 
		#endregion
	}
}
