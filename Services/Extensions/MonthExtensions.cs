using Core.Calendar;

namespace Services.Extensions
{
    public static class MonthExtensions
    {
        public static Month MakeMonthHolidaysCorrection(this Month month, IList<DateTime>? holidays)
        {
            if (holidays != null && holidays.Count > 0)
                for (int i = 0; i < month.DaysCount; i++)
                {
                    var currentDate = month[i];
                    if (holidays.Contains(currentDate.Date))
                        currentDate.Type = DayType.Holiday;
                }
            return month;
        }

        public static Month MakeUserDaysCorrections(this Month month, IList<Day>? daysToCorrect)
        {
            if (daysToCorrect != null && daysToCorrect.Count > 0)
                for (int i = 0; i < month.DaysCount; i++)
                {
                    var currentDate = month[i];
                    for (int j = 0; j < daysToCorrect.Count; j++)
                        if (currentDate.Date == daysToCorrect[j].Date)
                            currentDate.Type = daysToCorrect[j].Type;
                }
            return month;
        }
    }
}
