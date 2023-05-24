using Core.Calendar;

namespace Services.POCO
{
	public class SheetDay
	{
		public SheetDay(int year, int month, int day, DayType dayType, TimeIntervalType? periodType = null) { 
			this.Year = year; 
			this.Month = month; 
			this.DayNumber = day;
			this.DayType = dayType;
			this.PeriodType = periodType;

			this.Date = new DateTime(year, month, day);
		}

		public int Year { get; set; }

		public int Month { get; set; }

		public int DayNumber { get; set; }

		public DayOfWeek DayOfWeek => Date.DayOfWeek;

		public DateTime Date { get; private set; }

		public DayType DayType { get; set; }

		public TimeIntervalType? PeriodType { get; set; }
	}
}
