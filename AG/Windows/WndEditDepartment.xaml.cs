using AG.Services;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.Domains;
using System.Windows;

namespace AG.Windows
{
	/// <summary>
	/// Логика взаимодействия для WndEditDepartment.xaml
	/// </summary>
	public partial class WndEditDepartment : Window
    {
        private readonly UserAccount? user;
        private readonly IDepartmentsService departmentsService;

        public WndEditDepartment()
        {
            InitializeComponent();
            user = SessionService.User;

            this.departmentsService = ServiceLocator.Provider.GetService<IDepartmentsService>()!;
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
