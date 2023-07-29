using Core.Calendar;

namespace Services.Extensions
{
    public static class MonthExtensions
    {
        /// <summary>
        /// Исправляет дни месяца <see cref="Month"/>, отмечая их как нерабочие в соответствии со списком из <see cref="DateTime"/>
        /// </summary>
        /// <param name="month"></param>
        /// <param name="holidays"></param>
        /// <returns>Исправленный месяц</returns>
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

		/// <summary>
		/// Исправляет типы дней месяца <see cref="Month"/> в соответствии с типами дней, указанными во входном списке
		/// </summary>
		/// <param name="month">Корректируемый месяц</param>
		/// <param name="daysToCorrect">Список из <see cref="Day"/> с типами дней, для которых требуется внести исправление</param>
		/// <returns>Исправленный месяц</returns>
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
