using Core.Calendar;

namespace Services.Domains.ReportCard
{
	public class SheetDay : Day
	{
		public SheetDay(int year, int month, int day, DayType dayType, TimeIntervalType? periodType = null) : base(year, month, day, dayType)
		{
			DayType = dayType;
			PeriodType = periodType;
		}

		public DayType DayType { get; set; }

		public TimeIntervalType? PeriodType { get; set; }

		public float WorkingHours { get; set; } = 0f;
	}
}
