using AG.ViewModels.Forms;
using System.Windows;

namespace AG.Windows
{
	/// <summary>
	/// Логика взаимодействия для WndSheetViewer.xaml
	/// </summary>
	public partial class WndSheetViewer : Window
	{
		#region ctor
		public WndSheetViewer()
		{
			InitializeComponent();
			viewModel = (SheetViewViewModel)Resources["viewModel"];

			//Событие при загрузке формы
			this.Loaded += WndSheetViewer_Loaded;
		}
		#endregion ctor

		private SheetViewViewModel viewModel;

		private async void WndSheetViewer_Loaded(object sender, RoutedEventArgs e)
		{

			await viewModel!.InitializeViewAsync(grid);
		}

		private async void btnApplyDate_Click(object sender, RoutedEventArgs e)
		{
			await viewModel!.UpdateSheet();
		}

		private void btnCorrectionDays_Click(object sender, RoutedEventArgs e)
		{
			viewModel!.EditCorrectionDays();
        }
    }
}
