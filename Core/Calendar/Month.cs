using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Calendar
{
	public class Month
    {
		#region ctor
		public Month(int year = 0, int monthId = 0)
		{
			this.Id = monthId == 0 ? DateTime.Now.Month : monthId;
			this.Year = year == 0 ? DateTime.Now.Year : year;
		}
		#endregion

		/// <summary>
		/// Список дней для месяца
		/// </summary>
		private IList<Day> Days  = new List<Day>();

		#region properties
		/// <summary>
		/// Год месяца
		/// </summary>
		public int Year { get; set; }


        /// <summary>
        /// Идентификатор месяца 1..12
        /// </summary>
        public int Id { get; private set; }

        public Day this[int id]
        {
            get => Days[id];
            set
            {
                Days[id] = value;
            }
        }

        public int DaysCount => Days?.Count ?? 0;

        /// <summary>
        /// Количество рабочих дней в месяце
        /// </summary>
        public int WorkingDays => Days?.Where(d => d.Type == DayType.Working || d.Type == DayType.PreHoliday).Count() ?? 0;
		#endregion

		#region methods

        /// <summary>
        /// Добавление дня в список
        /// </summary>
        /// <param name="day"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddDay(Day? day)
        {
            if (day == null)
                throw new ArgumentNullException(nameof(day));
            Days.Add(day);
        }

        /// <summary>
        /// Добавления дней списком
        /// </summary>
        /// <param name="days"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(IList<Day>? days)
        {
            if (days == null) throw new ArgumentNullException(nameof(days));
            foreach(var day in days)
            {
                Days.Add(day);
            }
        }
		#endregion methods
	}
}
