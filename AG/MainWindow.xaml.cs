using AG.Commands;
using AG.ViewModels.Forms;
using AG.Windows;
using Core.Database.Entities;
using Services.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = (MainWindowViewModel)this.Resources["viewModel"];
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command.Equals(MainUICommands.cmdOpenFile))
                e.CanExecute = true;

            if (e.Command.Equals(MainUICommands.cmdOrganizationInfo))
                e.CanExecute = true;
            if (e.Command.Equals(MainUICommands.cmdDepartmentsList))
                e.CanExecute = true;
            if (e.Command.Equals(MainUICommands.cmdOpenEmployeesList))
                e.CanExecute = true;

            if (e.Command.Equals(MainUICommands.cmdGenerateSheet))
                e.CanExecute = true;
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == MainUICommands.cmdOpenEmployeesList)
            {
                new WndEmployeesList().ShowDialog();
                return;
            }
        }
    }
}
