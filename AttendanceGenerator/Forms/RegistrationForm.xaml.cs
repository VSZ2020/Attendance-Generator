using AttendanceGenerator.Controllers.Authoriation;
using AttendanceGenerator.Controllers.Database;
using AttendanceGenerator.Infrastructure.Logger;
using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Establishment;
using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AttendanceGenerator.Forms
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public List<Establishment> Establishments { get; set; }
        
        public RegistrationForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            tbErrors.Text = String.Empty;
            tbErrors.Visibility = Visibility.Collapsed;
            
            //Получаем список организаций
            Establishments = LoadEstablishments();
            cbEstablishment.DataContext = this;
            cbEstablishment.ItemsSource = Establishments;
            cbDepartment.SelectionChanged += CbDepartment_SelectionChanged;

            //Обработчики кнопок
            btnRegister.Click += BtnRegister_Click;
            btnDontHaveEstablishment.Click += BtnDontHaveEstablishment_Click;
            btnDontHaveDepartment.Click += BtnDontHaveDepartment_Click;
        }

        private void BtnDontHaveEstablishment_Click(object sender, RoutedEventArgs e)
        {
            /*
             * По-умолчанию новый пользователь должен иметь только право на создание учреждения
             */
            EstablishmentEditForm estForm = new EstablishmentEditForm();
            var result = estForm.ShowDialog();
            if (result.HasValue && result.Value)
            {
                //Если успешно завершено, то обновляем список учреждений
                //Establishments = LoadEstablishments();
                cbEstablishment.ItemsSource = LoadEstablishments();
            }
        }

        private void BtnDontHaveDepartment_Click(object sender, RoutedEventArgs e)
        {
            var establishmentIndex = cbEstablishment.SelectedIndex;
            if (establishmentIndex == -1)
            {
                MessageBox.Show("Для начала выберите учреждение/организацию");
                return;
            }
            var est = cbEstablishment.SelectedItem as Establishment;
            DepartmentEditForm depEditForm = new DepartmentEditForm(ItemOperationType.Add, est.Id);
            var editResult = depEditForm.ShowDialog();
            if (editResult.HasValue && editResult.Value)
            {
                //Запоминаем выбранное учреждение
                int selectedIndex = cbEstablishment.SelectedIndex;
                cbEstablishment.ItemsSource = LoadEstablishments();
                cbEstablishment.SelectedIndex = selectedIndex;
                //cbDepartment.ItemsSource = DepartmentsController.GetDepartments(est.Id);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {

            if (CheckInputs())
            {
                var username = tbUsername.Text;
                var login = tbLogin.Text;
                var password = tbPassword.Password;
                var email = tbEmail.Text;

                UserAccount account = new UserAccount()
                {
                    Username = username,
                    Login = login,
                    Password = password,
                    Email = email,
                    RoleId = 1
                };
                try
                {
                    account.EstablishmentId = (cbEstablishment.SelectedIndex > -1) ? (cbEstablishment.SelectedItem as Establishment).Id : null;
                    account.Departments = (cbDepartment.SelectedIndex > -1 && cbDepartment.SelectedIndex > -1) ? new List<Department>() { cbDepartment.SelectedItem as Department } : new List<Department>();
                    UserController.AddUserAccount(account);

                    MessageBox.Show(
                        "Регистрация успешно завершена! Теперь вы можете войти в систему",
                        "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex, "Registration error");
                    MessageBox.Show(
                        $"Не удалось зарегистрировать нового пользователя. Причина: {ex.Message}",
                        "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void CbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDepartment.IsEnabled = Establishments.Count > 0;
        }

        /// <summary>
        /// Проверяет поля ввода на корректность введенных значений
        /// </summary>
        /// <returns></returns>
        private bool CheckInputs()
        {
            StringBuilder errosMsg = new StringBuilder();
            var username = tbUsername.Text;
            var login = tbLogin.Text;
            var password = tbPassword.Password;
            var email = tbEmail.Text;


            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                errosMsg.AppendLine("Имя пользователя не может быть пустым");
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login))
            {
                errosMsg.AppendLine("Логин пользователя не может быть пустым");
            }
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                errosMsg.AppendLine("Поле с адресом почты пустое");
            }
            if(!CheckEmail(email))
            {
                errosMsg.AppendLine("Неверный адрес электронной почты");
            }
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                errosMsg.AppendLine("Поле пароля не может быть пустым");
            }
            if (password != tbRepeatPassword.Password)
            {
                errosMsg.AppendLine("Пароли не совпадают");
            }
            if (ContainsForbiddenSymbol(password))
            {
                errosMsg.AppendLine("Пароль содержит один или несколько недопустимых символов");
            }
            if (password.Length < 6)
            {
                errosMsg.AppendLine("Пароль должен быть длиннее 5 символов");
            }
            if (cbEstablishment.SelectedIndex == -1)
            {
                var result =MessageBox.Show("У Вас не будет возможности редактировать отделы организации, т.к. вы не выбрали её. Вы уверены, что не хотите прикрепиться к организации?",
                    "Внимание",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    errosMsg.AppendLine("Выберите организацию");
                }
            }
            if (cbEstablishment.SelectedIndex > -1 && cbDepartment.SelectedIndex == -1)
            {
                var result = MessageBox.Show("У Вас не будет возможности редактировать отдел организации, т.к. вы не выбрали его. Вы уверены, что не хотите выбрать отдел?",
                    "Внимание",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    errosMsg.AppendLine("Выберите отдел/подразделение");
                }
            }
            //if (cbEstablishment.SelectedIndex == -1)
            //{
            //    errosMsg += "Организация не выбрана\n";
            //}
            //if (cbDepartment.SelectedIndex == -1)
            //{
            //    errosMsg += "Отдел не выбран\n";
            //}    
            //Если есть ошибки,то отображаем их для пользователя
            if (errosMsg.Length > 0)
            {
                ShowError(errosMsg.ToString(), false);
                return false;
            }

            return CheckRegistration(username, login, email);
        }
        /// <summary>
        /// Проверка адреса почты на корректность
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool CheckEmail(string email)
        {
            string pattern = @"^[_a-zA-Z0-9]+@[a-zA-Z0-9_]+\.[a-zA-Z]+$";
            Regex rg = new Regex(pattern);
            return rg.Match(email).Success;
        }
        private bool ContainsForbiddenSymbol(string pswd)
        {
            List<char> forbiddenSymbols = new List<char> { '!', '\"',  '^', '№', ';', '%',':','(',')'};
            for (int i = 0; i < forbiddenSymbols.Count; i++)
                if (pswd.Contains(forbiddenSymbols[i]))
                    return true;
            return false;
        }
        /// <summary>
        /// Отображает ошибки полей ввода
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Append"></param>
        private void ShowError(string Message, bool Append)
        {
            tbErrors.Visibility = Visibility.Visible;
            tbErrors.Text = Append ? tbErrors.Text + "\n" + Message : Message;
        }

        private bool CheckRegistration(string username, string login, string email)
        {
            List<UserAccount> accs;
            using (ApplicationDbContext dbc = ApplicationDbContext.GetContext())
            {
                accs = dbc.Accounts.Where(a => a.Username == username || a.Login == login || a.Email == email).ToList();
            }
            if (accs.Count > 0)
            {
                StringBuilder errorMsg = new StringBuilder();
                foreach(var acc in accs)
                {
                    if (acc.Username == username)
                    {
                        errorMsg.AppendLine("Пользователь с таким именем уже существует");
                        break;
                    }
                    if (acc.Login == login)
                    {
                        errorMsg.AppendLine("Пользователь с таким логином уже существует");
                        break;
                    }
                    if (acc.Email == email)
                    {
                        errorMsg.AppendLine("На данный e-mail уже зарегистрирован другой пользователь");
                        break;
                    }
                }
                ShowError(errorMsg.ToString(), true);
                return false;
            }
            return true;
        }

        private List<Establishment> LoadEstablishments()
        {
            return EstablishmentController.GetEstablishments();
        }
    }
}
