using AG.ViewModels.Forms;
using Core.ViewModel;
using Services.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
