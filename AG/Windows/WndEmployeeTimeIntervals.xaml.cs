﻿using AG.ViewModels.Forms;
using Services.Domains;
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
			viewModel?.ShowPopup(ViewModels.ObjectOperationType.Edit);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sender == btnApply)
			{
				viewModel.ApplyPendingChanges();
				return;
			}

			if (sender == btnAddTimeInterval)
			{
				viewModel?.ShowPopup(ViewModels.ObjectOperationType.Add);
				return;
			}

			if (sender == btnEditTimeInterval)
			{
				viewModel?.ShowPopup(ViewModels.ObjectOperationType.Edit);
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

	}
}
