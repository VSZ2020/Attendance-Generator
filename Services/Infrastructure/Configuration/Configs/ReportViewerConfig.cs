namespace Services.Infrastructure.Configuration.Configs
{
	public class ReportViewerConfig : IConfig
	{
		public string Name => nameof(WorkingWeekConfig);

		/// <summary>
		/// Количество десятичных цифр в суммарном количестве часов
		/// </summary>
		public int DigitsCountInTotalHours = 1;

		/// <summary>
		/// Количество десятичных цифр в количестве часов для каждого дня
		/// </summary>
		public int DigitsCountInDayHours = 2;
	}
}
