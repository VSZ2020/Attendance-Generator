using AG.ViewModels.Forms;
using System.Windows;
using System.Windows.Controls;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEmployeesList.xaml
    /// </summary>
    public partial class WndEmployeesList : Window
    {
        private EmployeesListFormViewModel? viewModel;

        public WndEmployeesList()
        {
            InitializeComponent();

            viewModel = (EmployeesListFormViewModel)this.Resources["viewModel"];

			this.Loaded += WndEmployeesList_Loaded;
        }

		private void WndEmployeesList_Loaded(object sender, RoutedEventArgs e)
		{
			viewModel?.LoadDepartments();
			if (viewModel?.Departments.Count > 1) viewModel?.LoadEmployees(viewModel.SelectedDepartmentId);
		}

		private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel?.LoadEmployees(viewModel.SelectedDepartmentId);
        }
    }
}
