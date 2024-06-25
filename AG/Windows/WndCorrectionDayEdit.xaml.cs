using AG.WPF.ViewModels;
using AG.WPF.ViewModels.Forms;
using System.Windows;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndCorrectionDayEdit.xaml
    /// </summary>
    public partial class WndCorrectionDayEdit : Window
	{
		private readonly CorrectionDaysFormViewModel viewModel;

		public WndCorrectionDayEdit()
		{
			InitializeComponent();
			this.viewModel = (CorrectionDaysFormViewModel)Resources["viewModel"];
			this.Closed += (s, e) => viewModel.SaveCorrectionDays();
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sender == btnAdd)
			{
				viewModel?.ShowPopup(ObjectOperationType.Add);
				return;
			}
			if (sender == btnEdit)
			{
				viewModel?.ShowPopup(ObjectOperationType.Edit);
				return;
			}
			if (sender == btnRemove)
			{
				viewModel?.RemoveDay();
				return;
			}
			if (sender == btnPopupAccept)
			{
				viewModel.ClosePopup();
				return;
			}
			if (sender == btnPopupCancel)
			{
				viewModel.ClosePopup(ObjectOperationType.Cancel);
				return;
			}
			if(sender == btnImport)
			{
				viewModel.LoadCorrectionDays();
			}
		}

		private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			viewModel.ShowPopup(ObjectOperationType.Edit);
		}
	}
}
