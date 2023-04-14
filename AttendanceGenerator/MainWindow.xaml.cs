using AttendanceGenerator.Controllers.Database;
using AttendanceGenerator.Forms;
using AttendanceGenerator.Model.Session;
using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AttendanceGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Интерфейс взаимодействия с полями формы, в т.ч. с окном уведомлений
        private MainWndViewModel _viewModel;

        public UserSession CurrentSession { get; set; } = new UserSession();

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWndViewModel)this.Resources["mwViewModel"];
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        public async void Initialize()
        {
            await Task.Run(() =>
            {
                _viewModel.ShowMessage("Загрузка...", "Проверка регистрации программы");
                CheckProgramRegistration();
                _viewModel.InfoMessageText = "Загрузка базы данных";
                DBUtils.CheckDatabase();
                _viewModel.ClearMessage();
            });
            if (!CurrentSession.IsLoggedIn)
                AuthUser();
            SetUIBindings();
        }
        /// <summary>
        /// Настраивает отображение кнопок для администратора
        /// </summary>
        public void ShowAdministratorButtons()
        {
            //Menu buttons

            //Form buttons
        }

        private void CheckProgramRegistration()
        {
            Thread.Sleep(1000);
        }

        private void SetUIBindings()
        {
           
        }

        #region User auth region
        /// <summary>
        /// Авторизация пользователя в системе
        /// </summary>
        private void AuthUser()
        {
            AuthForm frmAuth = new AuthForm();
            frmAuth.ShowDialog();
            if (frmAuth.UserAcc != null)
                SuccessfullAuth(frmAuth.UserAcc);
            else
                DeniedAuth();
        }
        
        private void SuccessfullAuth(UserAccount acc)
        {
            //CurrentSession = new UserSession();
            CurrentSession.LogInUser(acc);
            //Отображаем имя пользователя на экране
            _viewModel.UserLoggedIn(acc);
        }
        private void DeniedAuth()
        {
            _viewModel.IsUsernameEnabled = true;
            //this.Close();
        }

        private void LogoutUser()
        {
            CurrentSession.Logout();
            _viewModel.UserLoggedOut();
        }
        #endregion User auth region

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CmdUserProfile_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void CmdAppPreferences_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void CmdExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void UsernameItem_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSession.Account == null)
                AuthUser();
        }

        private void UserLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutUser();
        }

        private void adminBtnAccounts_Click(object sender, RoutedEventArgs e)
        {
            UsersList uls = new UsersList();
            uls.ShowDialog();
        }
    }
}
