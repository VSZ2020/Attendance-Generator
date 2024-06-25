using AG.WPF.Domains;
using AG.WPF.ViewModels;
using AG.WPF.ViewModels.Forms;
using System.Windows;
using System.Windows.Input;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEmployeeTimeIntervals.xaml
    /// </summary>
    public partial class WndEmployeeTimeIntervals : Window
    {
        private EmployeeTimeIntervalsViewModel viewModel;

		#region ctor
		public WndEmployeeTimeIntervals(Employee employee)
        {
            InitializeComponent();
            viewModel = (EmployeeTimeIntervalsViewModel)Resources["viewModel"];
			viewModel?.LoadInterface(employee);
        }

		#endregion

		private void TimeIntervalsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			viewModel?.ShowPopup(ObjectOperationType.Edit);
		}

		#region Button_Click
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sender == btnApply)
			{
				var result = viewModel.ApplyPendingChanges().Result;
				if (result)
					this.Close();
				return;
			}

			if (sender == btnAddTimeInterval)
			{
				viewModel?.ShowPopup(ObjectOperationType.Add);
				return;
			}

			if (sender == btnEditTimeInterval)
			{
				viewModel?.ShowPopup(ObjectOperationType.Edit);
				return;
			}

			if (sender == btnRemoveTimeInterval)
			{
				viewModel?.RemoveTimeInterval();
				return;
			}

			if (sender == btnPopupAccept)
			{
				viewModel?.ClosePopup();
				return;
			}

			if (sender == btnPopupCancel)
			{
				viewModel?.ClosePopup(true);
				return;
			}
		} 
		#endregion

		private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
		{
			viewModel.FilterTimeIntervals();
        }
    }
}
