using Core.Calendar;
using Services.POCO;
using Services.POCO.ReportCard;

namespace Services.Generators.ReportCard
{
    public class ReportCardService
    {
        public const int HalfMonthDay = 15;
        #region ctor
        public ReportCardService() { }
        #endregion

        #region fields

        #endregion

        public Sheet GenerateReportCard(
            string name,
            int number)
        {
            throw new NotImplementedException();
        }

        #region Utils

        /// <summary>
        /// Возвращает сумму рабочих часов за весь месяц
        /// </summary>
        /// <param name="month"></param>
        /// <param name="weekHours"></param>
        /// <param name="shortDayWorkingHours"></param>
        /// <returns></returns>
        public float GetTotalHours(SheetMonth month, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f)
        {
            return GetTotalHours(1, month.DaysCount, month, weekHours, shortDayWorkingHours);
        }

        /// <summary>
        /// Возвращает сумму часов в указанном диапазоне дней для заданного месяца
        /// </summary>
        /// <param name="fromDay">Начальное число</param>
        /// <param name="toDay">Конечное число</param>
        /// <param name="month">Месяц, для которого выполняется расчет</param>
        /// <param name="weekHours">Словарь с расчасовкой по дням недели</param>
        /// <param name="shortDayWorkingHours">Количество часов в сокращенном рабочем дне</param>
        /// <returns>Суммарное количество часов в указанном диапазоне дат</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public float GetTotalHours(int fromDay, int toDay, SheetMonth month, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f)
        {
            if (fromDay > toDay)
                throw new ArgumentOutOfRangeException($"{nameof(fromDay)} > {nameof(toDay)}. Начальный день не может быть больше конечного");
            if (fromDay < 0 || toDay < 0 || toDay > month.DaysCount)
                throw new ArgumentOutOfRangeException($"Одно из указанных чисел дня имеет неправильное значение! От: {fromDay}, до: {toDay}");

            //Суммарное количество рабочих часов
            float sumHours = 0;
            for (int i = fromDay - 1; i < toDay; i++)
            {
                var day = month[i];
                if (day.DayType == DayType.DayOff || day.DayType == DayType.Holiday)
                    continue;

                //Если предпраздничный день, то рабочий день сокращенный
                if (day.DayType == DayType.PreHoliday)
                    sumHours += shortDayWorkingHours;
                else
                    sumHours += weekHours[day.DayOfWeek];
            }
            return sumHours;
        }
        #endregion
    }
}
