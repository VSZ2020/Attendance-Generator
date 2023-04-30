using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.POCO;
using SQLiteRepository;
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
    /// Логика взаимодействия для WndEditDepartment.xaml
    /// </summary>
    public partial class WndEditDepartment : Window
    {
        private readonly UserAccount? user;
        private readonly UserAccountService userAccountService;
        public WndEditDepartment()
        {
            InitializeComponent();
            user = SessionService.User;

            var provider = new ServiceCollection().BuildServiceProvider();
            this.userAccountService = new UserAccountService(
                provider.GetService<IAppItemsRepository>(),
                provider.GetService<IEstablishmentItemsRepository>());
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Название не может быть пустым", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
        }
    }
}
