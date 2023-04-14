using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AttendanceGenerator.Forms.Commands
{
    public static class MainWndCommands
    {
        public static RoutedUICommand cmdAppPreferences = new RoutedUICommand(
            "Preferences",
            "Preferences",
            typeof(MainWindow),
            new InputGestureCollection()
            {
                new KeyGesture(Key.P, ModifierKeys.Control)
            });
        public static RoutedUICommand cmdUserProfile = new RoutedUICommand(
            "User profile",
            "UserProfile",
            typeof(MainWindow));
        public static RoutedUICommand cmdExit = new RoutedUICommand(
            "Exit",
            "Exit",
            typeof(MainWindow));
    }
}
