namespace Services.Domains.ReportCard
{
	public class SheetRowModel: BaseDomain
	{
		public float Rate { get; set; }
		public string FullName { get; set; }
		public string Function { get; set; }

		public float HalfMonthHours { get; set; }
		public float TotalMonthHours { get; set; }

		public float ExpectedHalfMonthHours { get; set; }
		public float ExpectedTotalMonthHours { get; set; }

		/// <summary>
		/// Дополнительные данные по каждому дню месяца
		/// </summary>
		public Dictionary<string, object> Custom { get; set; }

		public SheetRowModel()
		{
			Custom = new Dictionary<string, object>();
		}
	}
}
