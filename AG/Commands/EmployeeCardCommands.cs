using AG.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AG.Commands
{
	public class EmployeeCardCommands
	{
		public static RoutedUICommand CmdEmployeeTimeInterval = new RoutedUICommand("Неявки", "TimeIntervals", typeof(WndEditEmployee));
	}
}
