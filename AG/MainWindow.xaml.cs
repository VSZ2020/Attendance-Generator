using AG.Commands;
using AG.ViewModels.Forms;
using AG.Windows;
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
			viewModel = (MainWindowViewModel)this.Resources["viewModel"];

			viewModel.LoadApplication();
		}
		#endregion

		#region fields
		private readonly MainWindowViewModel viewModel;

		#endregion fields

		#region Event Handlers
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
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == MainUICommands.cmdOpenEmployeesList)
            {
                new WndEmployeesList().ShowDialog();
                return;
            }
            if (e.Command == MainUICommands.cmdDepartmentsList)
            {
                new WndDepartments().ShowDialog();
                return;
            }

			if (e.Command == MainUICommands.cmdViewSheet)
			{
				new WndSheetViewer().ShowDialog();
				return;
			}
		}
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
