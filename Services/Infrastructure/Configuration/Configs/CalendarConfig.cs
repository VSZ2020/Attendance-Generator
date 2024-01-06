using Core.Calendar;

namespace Services.Infrastructure.Configuration.Configs
{
	public class CalendarConfig : IConfig
	{
		public string Name => nameof(CalendarConfig);

		public IList<Day> DaysToCorrect { get; set; } = new List<Day>();

		/// <summary>
		/// Возвращает список государственных праздников в году. Если <see cref="ICalendar"/> NULL, то используется текущий год
		/// </summary>
		/// <param name="calendar">Объект календаря</param>
		/// <returns>Список праздничных дней</returns>
		public IList<DateTime> GetStateHolidays(ICalendar? calendar = null)
		{
			int curYear = calendar?.CurrentYear ?? DateTime.Now.Year;

			return new List<DateTime>()
			{
				//Праздники в январе
				new DateTime(curYear, 1, 1),
				new DateTime(curYear, 1, 2),
				new DateTime(curYear, 1, 3),
				new DateTime(curYear, 1, 4),
				new DateTime(curYear, 1, 5),
				new DateTime(curYear, 1, 6),
				new DateTime(curYear, 1, 7),

				//Праздники в феврале
				new DateTime(curYear, 2, 23),

				//Праздники в марте
				new DateTime(curYear, 3, 8),

				//Праздники в мае
				new DateTime(curYear, 5, 1),
				new DateTime(curYear, 5, 9),
				
				//Праздники в июне
				new DateTime(curYear, 6, 12),

				//Праздники в ноябре
				new DateTime(curYear, 11, 4)
			};
		}
	}
}
