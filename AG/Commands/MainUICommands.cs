using System.Windows.Input;

namespace AG.Commands
{
    public class MainUICommands
    {
        public static RoutedUICommand cmdOpenFile = new RoutedUICommand(
            "Открыть",
            "Open",
            typeof(MainWindow)
            );
        public static RoutedUICommand cmdOpenEmployeesList = new RoutedUICommand(
            "Сотрудники",
            "EmployeeList",
            typeof(MainWindow)
            );
        public static RoutedUICommand cmdOrganizationInfo = new RoutedUICommand(
            "Сведения об организации",
            "EstablishmentInfo",
            typeof(MainWindow)
            );
        public static RoutedUICommand cmdDepartmentsList = new RoutedUICommand(
            "Подразделения",
            "DepartmentsList",
            typeof(MainWindow)
            );
        public static RoutedUICommand cmdGenerateSheet = new RoutedUICommand(
            "Сформировать табель",
            "MakeTabel",
            typeof(MainWindow)
            );

		public static RoutedUICommand cmdViewSheet = new RoutedUICommand(
			"Просмотр табеля",
			"ViewReport",
			typeof(MainWindow)
			);
	}
}
