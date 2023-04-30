using AG.Commands;
using AG.ViewModels.Forms;
using System.Windows;
using System.Windows.Input;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndDepartments.xaml
    /// </summary>
    public partial class WndDepartments : Window
    {
        private DepartmentsFormViewModel viewModel;
        public WndDepartments()
        {
            InitializeComponent();
            viewModel = (DepartmentsFormViewModel)Resources["ViewModel"];
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == DepartmentsCommands.cmdAddDepartment) e.CanExecute = true;
            if (e.Command == DepartmentsCommands.cmdEditDepartment) e.CanExecute = viewModel?.SelectedDepartment != null;
            if (e.Command == DepartmentsCommands.cmdRemoveDepartment) e.CanExecute = viewModel?.Departments.Count > 0;
            if (e.Command == DepartmentsCommands.cmdShowEmployees) e.CanExecute = viewModel?.SelectedDepartment != null;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
