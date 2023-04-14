using AttendanceGenerator.Controllers.Authoriation;
using AttendanceGenerator.Model.Session.UserAccount;
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

namespace AttendanceGenerator.Forms
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public UserAccount? UserAcc { get; set; }
        public AuthForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            btnForgotPassword.Click += BtnForgotPassword_Click;
            btnRegistration.Click += BtnRegistration_Click;

        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Если включить метод Hide(), то окно из диалогового станет обычным и не будет работать DialogResult
            //this.Hide();
            RegistrationForm rgForm = new RegistrationForm();
            rgForm.ShowDialog();
            //this.Show();
        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PasswordRecoveryForm recoveryForm = new PasswordRecoveryForm();
            recoveryForm.ShowDialog();
            this.Show();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var pass = tbPassword.Password;
            var user = UserController.AuthUser(login, pass);

            if (user != null)
            {
                UserAcc = user;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильно введен логин и пароль, либо такого пользователя не существует");
            }
        }
    }
}
