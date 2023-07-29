using AG.Commands;
using AG.Services;
using AG.ViewModels.Forms;
using AG.Windows;
using Core.Database.Entities;
using Microsoft.Extensions.DependencyInjection;
using Services.Calendar;
using Services.Database;
using Services.Infrastructure.Configuration;
using Services.Infrastructure.Configuration.Configs;
using Services.ReportCard;
using SQLiteRepository;
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

            
            var userAccountService = ServiceLocator.Provider.GetService<IUserAccountService>();

            SessionService.User = userAccountService.GetUserById(3).Results.First();

            viewModel.OnChanged(nameof(viewModel.Username));
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

			if (e.Command.Equals(MainUICommands.cmdViewSheet))
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
            if (e.Command == MainUICommands.cmdDepartmentsList)
            {
                new WndDepartments().ShowDialog();
                return;
            }

			if (e.Command == MainUICommands.cmdViewSheet)
			{
                var calendarService = ServiceLocator.Provider.GetService<ICalendarService>();
                var employeeService = ServiceLocator.Provider.GetService<IEmployeeService>();
                var reportCardService = ServiceLocator.Provider.GetService<IReportCardService>();
                var weekConfig = ConfigManager.Instance.Get<WorkingWeekConfig>();
                var reportViewerConfig = ConfigManager.Instance.Get<ReportViewerConfig>();

				new WndSheetViewer(calendarService, employeeService, reportCardService, weekConfig, reportViewerConfig).ShowDialog();
				return;
			}
		}
    }
}
