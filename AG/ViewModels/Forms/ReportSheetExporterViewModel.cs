using AG.WPF.Domains;
using AG.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.WPF.ViewModels.Forms
{
    public class ReportSheetExporterViewModel : ViewModelCore
    {
        public ReportSheetExporterViewModel()
        {

        }

        #region Fields
        private string formTypeId = "504421";
        private string name = "Без названия";

        private int number = 0;

        private int sheetType = 0;

        private int correctionNumber = 0;

        private string establishmentName = string.Empty;

        private string departmentName = string.Empty;

        private TimeInterval? accountingPeriod;

        private DateTime? date;

        private DateTime? generationTime;

        private string sheetResponsible = string.Empty;

        private string sheetExecutor = string.Empty;

        private string financialExecutor = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Номер формы по ОКУД
        /// </summary>
        public string FormTypeId { get => formTypeId; set { formTypeId = value; OnChanged(); } }

        /// <summary>
        /// Название табеля
        /// </summary>
        public string Name { get => name; set { name = value; OnChanged(); } }

        /// <summary>
        /// Номер табеля
        /// </summary>
        public int Number { get => number; set { number = value; OnChanged(); } }

        /// <summary>
        /// Вид табеля:
        /// 0 - первичный,
        /// 1 - корректирующий, и т.д.
        /// </summary>
        public int SheetType { get => sheetType; set { sheetType = value; OnChanged(); } }

        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get => correctionNumber; set { correctionNumber = value; OnChanged(); } }

        /// <summary>
        /// Учреждение
        /// </summary>
        public string EstablishmentName { get => establishmentName; set { establishmentName = value; OnChanged(); } }

        /// <summary>
        /// Отдел, для которого генерируется табель
        /// </summary>
        public string DepartmentName { get => departmentName; set { departmentName = value; OnChanged(); } }

        /// <summary>
        /// Период ведения табеля
        /// </summary>
        public TimeInterval? AccountingPeriod { get => accountingPeriod; set { accountingPeriod = value; OnChanged(); } }

        /// <summary>
        /// Дата по ОКПО
        /// </summary>
        public DateTime? Date { get => date; set { date = value; OnChanged(); } }

        /// <summary>
        /// Дата формирования табеля
        /// </summary>
        public DateTime? GenerationTime { get => generationTime; set { generationTime = value; OnChanged(); } }

        /// <summary>
        /// Ответственный за табель
        /// </summary>
        public string? SheetResponsible { get => sheetResponsible; set { sheetResponsible = value; OnChanged(); } }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public string SheetExecutor { get => sheetExecutor; set { sheetExecutor = value; OnChanged(); } }

        /// <summary>
        /// Финансовый исполнитель (проверяющий бухгалтер)
        /// </summary>
        public string FinancialExecutor { get => financialExecutor; set { financialExecutor = value; OnChanged(); } }
        #endregion

        public void InitializeViewModel()
        {

        }
    }
}
