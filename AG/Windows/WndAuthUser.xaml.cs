using AG.WPF.ViewModels.Forms;
using System.Windows;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndAuthUser.xaml
    /// </summary>
    public partial class WndAuthUser : Window
	{
		#region ctor
		public WndAuthUser()
		{
			InitializeComponent();
			viewModel = (AuthFormViewModel)Resources["viewModel"];
		}
		#endregion

		private readonly AuthFormViewModel viewModel;

		private void btnRegister_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (viewModel.AuthUser(tbLogin.Text, tbPassword.Password))
				this.Close();
		}
	}
}
