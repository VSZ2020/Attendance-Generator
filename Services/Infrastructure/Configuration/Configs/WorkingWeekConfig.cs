using Core.Calendar;

namespace Services.Infrastructure.Configuration.Configs
{
	public class WorkingWeekConfig : IConfig
	{
		public string Name => nameof(WorkingWeekConfig);

		public int HoursInDay { get; set; } = 8;
		public int HoursInShortDay { get; set; } = 7;

		/// <summary>
		/// Словарь, в котором сопоставлены дни недели и тип дня (выходной или рабочий). Актуально для нестандартных рабочих дней
		/// </summary>
		public Dictionary<DayOfWeek, DayType> DayTypes { get; set; } = new Dictionary<DayOfWeek, DayType>() {
			{ DayOfWeek.Monday,		DayType.Working },
			{ DayOfWeek.Tuesday,	DayType.Working },
			{ DayOfWeek.Wednesday,	DayType.Working },
			{ DayOfWeek.Thursday,	DayType.Working },
			{ DayOfWeek.Friday,		DayType.Working },
			{ DayOfWeek.Saturday,	DayType.DayOff },
			{ DayOfWeek.Sunday,		DayType.DayOff },
		};
	}
}
