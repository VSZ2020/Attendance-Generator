using AG.Commands;
using AG.ViewModels.Forms;
using Services.Domains;
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
