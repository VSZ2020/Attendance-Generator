using Core.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Calendar
{
	public class CalendarService
	{
		#region ctor
		public CalendarService(ICalendar? calendar) { this.calendar = calendar; }
		#endregion ctor

		#region fields
		private readonly ICalendar? calendar;


		#endregion fields

		public int GetHolidaysCount()
		{
			return GetHolidaysCount(this.calendar);
		}

		public static int GetHolidaysCount(ICalendar? calendar)
		{
			return calendar?.Days?.Where(d => d.Type == DayType.Holiday).Count() ?? 0;
		}

		public static int GetHolidaysCount(IList<Day>? days)
		{
			return days?.Where(d => d.Type == DayType.Holiday).Count() ?? 0;
		}

		#region Utils
		/// <summary>
		/// Возвращает список праздников в году. Если <see cref="ICalendar"/> NULL, то используется текущий год
		/// </summary>
		/// <param name="calendar">Объект календаря</param>
		/// <returns>Список праздничных дней</returns>
		public static IList<Day> GetDefaultHolidays(ICalendar? calendar = null)
		{
			int curYear = calendar?.CurrentYear ?? DateTime.Now.Year;

			return new List<Day>()
			{
				//Праздники в январе
				new Day(1, 1, curYear, DayType.Holiday),
				new Day(2, 1, curYear, DayType.Holiday),
				new Day(3, 1, curYear, DayType.Holiday),
				new Day(4, 1, curYear, DayType.Holiday),
				new Day(5, 1, curYear, DayType.Holiday),
				new Day(6, 1, curYear, DayType.Holiday),
				new Day(7, 1, curYear, DayType.Holiday),

				//Праздники в феврале
				new Day(23, 2, curYear, DayType.Holiday),

				//Праздники в марте
				new Day(8, 3, curYear, DayType.Holiday),

				//Праздники в мае
				new Day(1, 5, curYear, DayType.Holiday),
				new Day(9, 5, curYear, DayType.Holiday),
				
				//Праздники в июне
				new Day(1, 6, curYear, DayType.Holiday),
				new Day(12, 6, curYear, DayType.Holiday),

				//Праздники в ноябре
				new Day(4, 11, curYear, DayType.Holiday),
			};
		}
		#endregion
	}
}
