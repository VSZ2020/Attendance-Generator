using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Services.Domains.ReportCard
{
	public class Sheet
	{
		public Sheet(IList<SheetRowModel>? rows = null) 
		{ 
			if (rows == null) rows = new List<SheetRowModel>();
		}

		#region Properties
		/// <summary>
		/// Номер формы по ОКУД
		/// </summary>
		[JsonProperty]		
		public string FormTypeId { get; set; }

        /// <summary>
        /// Название табеля
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Номер табеля
        /// </summary>
        [JsonProperty]
        public int Number { get; set; }

        /// <summary>
        /// Вид табеля:
        /// 0 - первичный,
        /// 1 - корректирующий, и т.д.
        /// </summary>
        [JsonProperty]
        public int SheetType { get; set; } = 0;

        /// <summary>
        /// Номер корректировки
        /// </summary>
        [JsonProperty]
        public int CorrectionNumber { get; set; }

        /// <summary>
        /// Учреждение
        /// </summary>
        [JsonProperty]
        public string EstablishmentName { get; set; }

        /// <summary>
        /// Отдел, для которого генерируется табель
        /// </summary>
        [JsonProperty]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Период ведения табеля
        /// </summary>
        [JsonProperty]
        public TimeInterval? AccountingPeriod { get; set; }

        /// <summary>
        /// Дата по ОКПО
        /// </summary>
        [JsonProperty]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Дата формирования табеля
        /// </summary>
        [JsonProperty]
        public DateTime? GenerationTime { get; set; }

        /// <summary>
        /// Ответственный за табель
        /// </summary>
        [JsonProperty]
        public string? SheetResponsible { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [JsonProperty]
        public string SheetExecutor { get; set; }

        /// <summary>
        /// Финансовый исполнитель (проверяющий бухгалтер)
        /// </summary>
        [JsonProperty]
        public string FinancialExecutor { get; set; }

        [JsonProperty]
        public IList<SheetRowModel> HoursData { get; set; }
		#endregion
	}
}
