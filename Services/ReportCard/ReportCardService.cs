using Core.Calendar;
using Services.Calendar;
using Services.Domains;
using Services.Domains.ReportCard;
using Services.Infrastructure.Configuration.Configs;
using System;

namespace Services.ReportCard
{
	public class ReportCardService: IReportCardService
    {
        public const int HalfMonthDay = 15;
        #region ctor
        public ReportCardService(ICalendarService calendarService) {
            this.calendarService = calendarService;   
        }
        #endregion

        #region fields
        private readonly ICalendarService calendarService;
        #endregion

        public Sheet GenerateReportCard(
            string name,
            int number)
        {
            throw new NotImplementedException();
        }

        public IList<SheetRowModel> MakeViewSheetRows(IList<Employee> employees, DateTime currentDate, IList<DateTime> holidays, IList<Day> daysToCorrect, WorkingWeekConfig weekConfig, ReportViewerConfig reportViewerConfig)
        {
			//Получаем базовый месяц для указанной даты
			var baseMonth = calendarService!.CreateMonth(currentDate, weekConfig, holidays, daysToCorrect);
			var listOfEmployeeMonth = calendarService.MakeManyEmployeesMonths(baseMonth, weekConfig, employees);

			IList<SheetRowModel> rows = new List<SheetRowModel>();
			for (int i = 0; i < employees.Count; i++)
			{
                var sheetMonth = listOfEmployeeMonth[i];
                var halfMonthEmployeeHours = GetSumOfHours(1, weekConfig.MonthHalfDate, sheetMonth, weekConfig);
                var totalMonthEmployeeHours = GetSumOfHours(1, sheetMonth.DaysCount, sheetMonth, weekConfig);

				Dictionary<string, object> properties = GenerateEmployeeProperties(sheetMonth, reportViewerConfig);

				rows.Add(new SheetRowModel()
				{
					Id = employees[i].Id,
					FullName = employees[i].FullName,
					Function = employees[i].Function,
					Rate = employees[i].Rate,
					HalfMonthHours = halfMonthEmployeeHours,
					TotalMonthHours = totalMonthEmployeeHours,
					Custom = properties
				});
			}
			return rows;
		}

        #region Utils

        public float GetTotalHours(IList<Day> days, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f)
        {
            return GetSumOfHours(1, days.Count, days, weekHours, shortDayWorkingHours);
        }

        public float GetSumOfHours(int fromDay, int toDay, IList<Day> days, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f)
        {
            if (fromDay > toDay)
                throw new ArgumentOutOfRangeException($"{nameof(fromDay)} > {nameof(toDay)}. Начальный день не может быть больше конечного");
            if (fromDay < 0 || toDay < 0 || toDay > days.Count)
                throw new ArgumentOutOfRangeException($"Одно из указанных чисел дня имеет неправильное значение! От: {fromDay}, до: {toDay}");

            //Суммарное количество рабочих часов
            float sumHours = 0;
            for (int i = fromDay - 1; i < toDay; i++)
            {
                var day = days[i];
                if (day.Type == DayType.DayOff || day.Type == DayType.Holiday)
                    continue;

                //Если предпраздничный день, то рабочий день сокращенный
                if (day.Type == DayType.PreHoliday)
                    sumHours += shortDayWorkingHours;
                else
                    sumHours += weekHours[day.DayOfWeek];
            }
            return sumHours;
        }

		public float GetSumOfHours(int fromDay, int toDay, SheetMonth month, WorkingWeekConfig weekConfig)
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
				if (day.Type == DayType.DayOff || day.Type == DayType.Holiday)
					continue;

				sumHours += day.WorkingHours;
			}
			return sumHours;
		}

		private Dictionary<string, object> GenerateEmployeeProperties(SheetMonth month, ReportViewerConfig reportViewerConfig)
		{
			Dictionary<string, object> prop = new Dictionary<string, object>();
			for (int j = 0; j < month.DaysCount; j++)
			{
				string name = DayToString(month[j], reportViewerConfig);
				if (month[j].PeriodType != null)
					name = month[j].PeriodType!.ShortName;
				prop[(j + 1).ToString()] = name;
			}

			return prop;
		}

		private string DayToString(SheetDay day, ReportViewerConfig reportViewerConfig)
		{
			string hours = string.Format("{0:F" + reportViewerConfig.DigitsCountInDayHours.ToString() + "}", day.WorkingHours);
			return day.DayType switch
			{
				DayType.PreHoliday => hours,
				DayType.Working => hours,
				DayType.Holiday => "B",
				DayType.DayOff => "В"
			};
		}

		//public float GetHoursForDayType(DayType dayType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig)
		//{
		//    return dayType switch
		//    {
		//        DayType.DayOff | DayType.Holiday => 0,
		//        DayType.PreHoliday => weekConfig.HoursInShortDay,
		//        DayType.Working => weekConfig.WorkingHours[dayOfWeek],
		//        _ => 0
		//    } ;
		//}

		///// <summary>
		///// Содержит правила обработки для пользовательских временных интервалов
		///// </summary>
		///// <param name="intervalType">Тип пользовательского временнОго интервала</param>
		///// <param name="dayOfWeek">День недели</param>
		///// <param name="weekConfig">Конфигурация для недели</param>
		///// <returns>Количество часов, соотвествующих пользовательскому типу временного интервала</returns>
		//public float GetCustomTimeIntervalHours(TimeIntervalType? intervalType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig)
		//{
		//    if (intervalType == null)
		//        return 0;
		//    return intervalType.ShortName switch
		//    {
		//        "К" => weekConfig.WorkingHours[dayOfWeek],
		//        _ => 0f
		//    };
		//}
		#endregion
	}
}
