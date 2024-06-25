using AG.Windows;
using AG.WPF.Database;
using AG.WPF.Infrastructure;
using AG.WPF.Session;
using AG.WPF.ViewModel;
using Services;
using Services.Domains;
using System;
using System.Linq;
using System.Windows;

namespace AG.WPF.ViewModels.Forms
{
    public class MainWindowViewModel : ViewModelCore
    {
        #region ctor
        public MainWindowViewModel()
        {
            departmentsService = ServicesLocator.GetService<IDepartmentsService>()!;

            SetEstablishment();
            LoadApplication();
        }
        #endregion ctor

        #region fields
        private readonly IDepartmentsService departmentsService;

        private bool isLoggedIn = false;
        private string username = string.Empty;
        private string windowTitle = "";
        #endregion

        #region Properties
        public string Username { get => username; set { username = value; OnChanged(); } }
        public bool IsLoggedIn { get => isLoggedIn; set { isLoggedIn = value; OnChanged(); } }

        public string Title { get => "Attendance Generator" + (!string.IsNullOrEmpty(windowTitle) ? " - " + windowTitle : ""); set { windowTitle = value; OnChanged(); } }
        public Guid EstablishmentId { get; set; } = Guid.Empty;
        #endregion properties

        public void LoadApplication()
        {
            IsLoggedIn = true;
            //SessionService.User = new UserAccount("Test user");
        }

        #region LoginUser
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
        #endregion

        /// <summary>
        /// Выход пользователя из системы
        /// </summary>
        public void LogoutUser()
        {
            SessionService.User = null;
            IsLoggedIn = false;
        }

        public void ShowDepartmentsForm()
        {
            new WndDepartments(EstablishmentId).ShowDialog();
        }

        public async void ShowOrganizationForm()
        {
            var establishment = await departmentsService.GetEstablishmentByIdAsync(EstablishmentId);
            new WndEditEstablishment(establishment).ShowDialog();
        }

        public void ShowEmployeesList()
        {
            new WndEmployeesList().ShowDialog();
        }

        public void ShowReportForm()
        {
            new WndSheetViewer().ShowDialog();
        }

        public void ShowUserAccountsForm()
        {
            throw new NotImplementedException();
        }

        public void SetEstablishment()
        {
            var establishment = departmentsService.GetEstablishmentsAsync().Result.First();

            EstablishmentId = establishment.Id;
            Title = establishment.Name;

            SessionService.CurrentEstablishemntId = EstablishmentId;
        }
    }
}
