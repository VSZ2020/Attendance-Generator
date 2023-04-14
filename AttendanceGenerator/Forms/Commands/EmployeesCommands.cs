using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AttendanceGenerator.Forms.Commands
{
    public static class EmployeesCommands
    {
        /// <summary>
        /// Команда добавления сотрудника
        /// </summary>
        public static RoutedUICommand CmdNewEmployee = new RoutedUICommand(
            /*Content*/
            "New employee",
            /*Name*/
            "NewEmployee",
            typeof(EmployeesEditorForm),
            new InputGestureCollection()
            {
                new KeyGesture(Key.N, ModifierKeys.Control)
            });

        /// <summary>
        /// Команда удаления сотрудника
        /// </summary>
        public static RoutedUICommand CmdRemoveEmployee = new RoutedUICommand(
            /*Content*/
            "Delete employee",
            /*Name*/
            "RemoveEmployee",
            typeof(EmployeesEditorForm),
            new InputGestureCollection()
            {
                new KeyGesture(Key.R, ModifierKeys.Control)
            });

        /// <summary>
        /// Команда редактирования сотрудника
        /// </summary>
        public static RoutedUICommand CmdEditEmployee = new RoutedUICommand(
            /*Content*/
            "Edit employee",
            /*Name*/
            "EditEmployee",
            typeof(EmployeesEditorForm),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control)
            });

        /// <summary>
        /// Команда редактирования сотрудника
        /// </summary>
        public static RoutedUICommand CmdSearchEmployee = new RoutedUICommand(
            /*Content*/
            "Search employee",
            /*Name*/
            "SearchEmployee",
            typeof(EmployeesEditorForm),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            });

        /// <summary>
        /// Команда экспорта списка сотрудников
        /// </summary>
        public static RoutedUICommand CmdExportEmployees = new RoutedUICommand(
            /*Content*/
            "Export employees",
            /*Name*/
            "ExportEmployees",
            typeof(EmployeesEditorForm),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D, ModifierKeys.Control)
            });
    }
}
