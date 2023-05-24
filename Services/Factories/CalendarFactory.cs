using Core.Calendar;

namespace Services.Factories
{
    public class CalendarFactory
    {
        public static Day CreateDay(DateTime date, DayType type)
        {
            return new Day(date.Year, date.Month, date.Day, type);
        }

        public static Month CreateMonth(DateTime date)
        {
            return new Month(date.Year, date.Month);
        }

        public static Month CreateMonth(int year, int month)
        {
            return new Month(year, month);
        }
    }
}
