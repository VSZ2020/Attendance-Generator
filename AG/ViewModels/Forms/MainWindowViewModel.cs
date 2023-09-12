using AG.Services;
using AG.Windows;
using Core.ViewModel;
using Services.Domains;
using System.Windows;

namespace AG.ViewModels.Forms
{
	public class MainWindowViewModel: ViewModelCore
    {
		#region ctor
		public MainWindowViewModel()
		{

		}
		#endregion ctor

		#region fields
		private bool isLoggedIn = false;
		private string username = string.Empty;
		#endregion
		#region Properties
		public string Username { get => username; set { username = value; OnChanged(); } }

        public int EstablishmentId { get; set; } = 1;
		public bool IsLoggedIn { get => isLoggedIn; set { isLoggedIn = value; OnChanged(); } }
        #endregion properties

		public void LoadApplication()
		{
			IsLoggedIn = true;
			SessionService.User = new UserAccount("Test user");
		}

		/// <summary>
		/// Вход пользователя в систему
		/// </summary>
		public void LoginUser()
		{
			var wndAuthUser = new WndAuthUser();
			wndAuthUser.ShowDialog();

			if (SessionService.IsLoggedIn)
			{
				IsLoggedIn = true;
				Username = SessionService.User.Username;
			}
			else
			{
				MessageBox.Show("Не удалось пройти аутентификацию пользователя!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

			}
		}

		/// <summary>
		/// Выход пользователя из системы
		/// </summary>
		public void LogoutUser()
		{
			SessionService.User = null;
			IsLoggedIn = false;
		}
    }
}
