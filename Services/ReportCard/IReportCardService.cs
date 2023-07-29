using Core.Calendar;
using Services.Domains;
using Services.Domains.ReportCard;
using Services.Infrastructure.Configuration.Configs;

namespace Services.ReportCard
{
	public interface IReportCardService
	{
		/// <summary>
		/// Возвращает сумму рабочих часов за весь месяц
		/// </summary>
		/// <param name="month"></param>
		/// <param name="weekHours"></param>
		/// <param name="shortDayWorkingHours"></param>
		/// <returns></returns>
		public float GetTotalHours(IList<Day> days, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f);

		/// <summary>
		/// Возвращает сумму часов в указанном диапазоне дней для заданного месяца
		/// </summary>
		/// <param name="fromDay">Начальное число</param>
		/// <param name="toDay">Конечное число</param>
		/// <param name="days">Перечень дней для которых выполняется расчет</param>
		/// <param name="weekHours">Словарь с расчасовкой по дням недели</param>
		/// <param name="shortDayWorkingHours">Количество часов в сокращенном рабочем дне</param>
		/// <returns>Суммарное количество часов в указанном диапазоне дат</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public float GetSumOfHours(int fromDay, int toDay, IList<Day> days, Dictionary<DayOfWeek, float> weekHours, float shortDayWorkingHours = 7f);

		//public float GetHoursForDayType(DayType dayType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig);

		//public float GetCustomTimeIntervalHours(TimeIntervalType? intervalType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig);

		public float GetSumOfHours(
			int fromDay, int toDay,
			SheetMonth month, WorkingWeekConfig weekConfig);

		public IList<SheetRowModel> MakeViewSheetRows(IList<Employee> employees, DateTime currentDate, IList<DateTime> holidays, IList<Day> daysToCorrect, WorkingWeekConfig weekConfig, ReportViewerConfig reportViewerConfig);
	}
}
