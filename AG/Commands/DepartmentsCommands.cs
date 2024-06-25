using AG.Windows;
using System.Windows.Input;

namespace AG.WPF.Commands
{
    public class DepartmentsCommands
    {
        public static RoutedUICommand cmdAddDepartment = new RoutedUICommand(
            "Добавить",
            "Add",
            typeof(WndDepartments));

        public static RoutedUICommand cmdRemoveDepartment = new RoutedUICommand(
            "Удалить",
            "Remove",
            typeof(WndDepartments));

        public static RoutedUICommand cmdEditDepartment = new RoutedUICommand(
            "Редактировать",
            "Edit",
            typeof(WndDepartments));

        public static RoutedUICommand cmdShowEmployees = new RoutedUICommand(
            "Сотрудники",
            "Employees",
            typeof(WndDepartments));

    }
}
