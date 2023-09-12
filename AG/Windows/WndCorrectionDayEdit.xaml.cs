using AG.ViewModels.Forms;
using Core.Calendar;
using System.Collections.Generic;
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
		}

		/// <summary>
		/// Возвращает список поправочных дней
		/// </summary>
		/// <returns></returns>
		public IList<Day> GetCorrectionDays()
		{
			return viewModel.GetCorrectionDays();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (sender == btnAdd)
			{
				viewModel?.ShowPopup(ViewModels.ObjectOperationType.Add);
				return;
			}
			if (sender == btnEdit)
			{
				viewModel?.ShowPopup(ViewModels.ObjectOperationType.Edit);
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
				viewModel.ClosePopup(ViewModels.ObjectOperationType.Cancel);
				return;
			}
			if(sender == btnImport)
			{
				viewModel.LoadFromFile();
			}
		}

		private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			viewModel.ShowPopup(ViewModels.ObjectOperationType.Edit);
		}
	}
}
