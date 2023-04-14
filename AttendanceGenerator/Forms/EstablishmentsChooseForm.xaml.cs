using AttendanceGenerator.Model.Establishment;
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
    /// Логика взаимодействия для EstablishmentsChooseForm.xaml
    /// </summary>
    public partial class EstablishmentsChooseForm : Window
    {
        public List<Establishment> Establishments { get; set; }
        private UserAccount Account;

        public EstablishmentsChooseForm(UserAccount account)
        {
            InitializeComponent();
            Account = account;
        }
    }
}
