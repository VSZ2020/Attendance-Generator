using AG.WPF.ViewModels.Forms;
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
    /// Логика взаимодействия для WndReportSheetExporter.xaml
    /// </summary>
    public partial class WndReportSheetExporter : Window
    {
        public WndReportSheetExporter()
        {
            InitializeComponent();

            vm = new ReportSheetExporterViewModel();
            DataContext = vm;

            vm.InitializeViewModel();
        }

        private ReportSheetExporterViewModel vm;
    }
}
