using AG.Windows;
using System.Windows.Input;

namespace AG.WPF.Commands
{
    public class EmployeesListCommands
    {
        public static RoutedUICommand cmdAddEmployee = new RoutedUICommand(
            "Добавить",
            "Add",
            typeof(WndEmployeesList));

        public static RoutedUICommand cmdRemoveEmployee = new RoutedUICommand(
            "Удалить",
            "Remove",
            typeof(WndEmployeesList));

        public static RoutedUICommand cmdEditEmployee = new RoutedUICommand(
            "Редактировать",
            "Edit",
            typeof(WndEmployeesList));

        public static RoutedUICommand cmdTimeIntervals = new RoutedUICommand(
            "Неявки сотрудника",
            "TimeIntervals",
            typeof(WndEmployeesList));

    }
}
