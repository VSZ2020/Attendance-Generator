using AttendanceGenerator.Controllers.Authoriation;
using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для UsersList.xaml
    /// </summary>
    public partial class UsersList : Window
    {
        public ObservableCollection<UserAccount> Accounts { get; set; } = new ObservableCollection<UserAccount>();
        public UserAccount SelectedAccount { get; set; }
        public UsersList()
        {
            InitializeComponent();
        }

        public void LoadUsers()
        {
            var users = UserController.GetAll();
            foreach (var user in users)
                Accounts.Add(user);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            LoadUsers();
        }
    }
}
