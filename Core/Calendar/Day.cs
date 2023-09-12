using System;

namespace Core.Calendar
{
	public class Day
    {
        public DateTime Date { get; private set; }

        public int Month => Date.Month;
        public int Year => Date.Year;

        public int DayNumber => Date.Day;

        public DayOfWeek DayOfWeek => Date.DayOfWeek;

        /// <summary>
        /// Задает тип дня: выходной, праздничный, предпраздничный
        /// </summary>
        public DayType Type { get; set; }

        public Day(int year, int month, int day, DayType type): this(new DateTime(year, month, day), type){}

		public Day(DateTime date, DayType type)
		{
			this.Date = date;
			this.Type = type;

		}
	}
}
