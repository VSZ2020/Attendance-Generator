using AG.WPF.ViewModels.Forms;
using Services.Domains;
using System.Windows;

namespace AG.Windows
{
    /// <summary>
    /// Логика взаимодействия для WndEditEmployee.xaml
    /// </summary>
    public partial class WndEditEmployee : Window
	{
		public WndEditEmployee(Employee? employee = null)
		{
			InitializeComponent();

			vm = new EditEmployeeViewModel(employee);
			DataContext = vm;

			//CommandBindings.Add(new CommandBinding(EmployeeCardCommands.CmdEmployeeTimeInterval, (s, e) => vm.ShowEmployeeTimeIntervals(), (s, e) => e.CanExecute = true)) ;
		}

		private EditEmployeeViewModel vm;

		private async void btnAccept_Click(object sender, RoutedEventArgs e)
		{
			if (await vm.AcceptChanges())
				this.Close();
        }
    }
}
