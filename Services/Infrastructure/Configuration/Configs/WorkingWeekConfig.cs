using Core.Calendar;

namespace Services.Infrastructure.Configuration.Configs
{
	public class WorkingWeekConfig : IConfig
	{
		public string Name => nameof(WorkingWeekConfig);
		
		/// <summary>
		/// Количество часов в полном рабочем дне
		/// </summary>
		public float HoursInDay { get; set; } = 8.25f;

		/// <summary>
		/// Количество часов в сокращенном рабочем дне
		/// </summary>
		public float HoursInShortDay { get; set; } = 7f;

		/// <summary>
		/// Распределение рабочих часов на неделе
		/// </summary>
		public Dictionary<DayOfWeek, float> WorkingHours { get; set; } = new() 
		{
			{ DayOfWeek.Monday,  8f},
			{ DayOfWeek.Tuesday,  8f},
			{ DayOfWeek.Wednesday,  8f},
			{ DayOfWeek.Thursday,  8f},
			{ DayOfWeek.Friday,  7f},
			{ DayOfWeek.Saturday,  0},
			{ DayOfWeek.Sunday,  0},
		};

		/// <summary>
		/// Словарь, в котором указываются рабочие и нерабочие дни в организации. По умолчанию задана 5-дневная рабочая неделя.
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
