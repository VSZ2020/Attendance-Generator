using AG.WPF.ViewModels.Forms;
using Services.Domains;
using System.Windows;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEditEstablishment.xaml
    /// </summary>
    public partial class WndEditEstablishment : Window
	{
		private EditEstablishmentViewModel viewModel;
		public WndEditEstablishment(Establishment? est = null)
		{
			InitializeComponent();

			viewModel = new EditEstablishmentViewModel(est);
			DataContext = viewModel;
		}

		private async void btnApply_Click(object sender, RoutedEventArgs e)
		{
			if (await viewModel.ApplyChanges())
				this.Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
