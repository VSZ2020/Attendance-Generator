using Core.Calendar;
using Services.Domains;
using Services.Domains.ReportCard;
using Services.Infrastructure.Configuration.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Calendar
{
	public interface ICalendarService
	{
		public int GetHolidaysCount();
		public int GetHolidaysCount(ICalendar? calendar);
		public int GetHolidaysCount(IList<Day>? days);

		public Month CreateMonth(
			DateTime date,
			WorkingWeekConfig? weekConfig,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null);

		public Month CreateMonth(
			DateTime date,
			Dictionary<DayOfWeek, DayType> weekDayTypes,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null);

		public Month CreateMonth(
			int year, int month,
			WorkingWeekConfig? weekConfig,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null);

		/// <summary>
		/// Создает объект <see cref="Month"/> на основе указанного года, месяца, рабочих часов по дням недели.
		/// </summary>
		/// <param name="year">Год, для которого формируется месяц</param>
		/// <param name="month">Номер создаваемого месяца [1..12]</param>
		/// <param name="weekDayTypes">Словарь с рабочими часами по каждому дню недели <seealso cref="DayOfWeek"/></param>
		/// <param name="holidays">Список дат, которые будут помечены как нерабочие дни</param>
		/// <param name="userDaysCorrections">Список из <see cref="Day"/>, 
		/// по которому будут внесены правки в <see cref="DayType"/> для дней формируемого месяца</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public Month CreateMonth(
			int year, int month,
			Dictionary<DayOfWeek,DayType> weekDayTypes,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null);

		/// <summary>
		/// Формирует <see cref="SheetMonth"/> для сотрудника
		/// </summary>
		/// <param name="currentMonth">Месяц, на основе которого будет сформирован <see cref="SheetMonth"/></param>
		/// <param name="employee">Сотрудник</param>
		/// <returns>Объект <see cref="SheetMonth"/></returns>
		public SheetMonth MakeEmployeeMonth(Month currentMonth, WorkingWeekConfig weekConfig, Employee employee);

		/// <summary>
		/// Формирует <see cref="SheetMonth"/> для сотрудника в соответствии с заданным месяцем <see cref="Month"/>
		/// </summary>
		/// <param name="currentMonth">Месяц, для которого формируется новый объект</param>
		/// <param name="employeePeriods"Временные интервалы сотрудника</param>
		/// <returns></returns>
		public SheetMonth MakeEmployeeMonth(Month currentMonth, WorkingWeekConfig weekConfig, IList<TimeInterval> employeePeriods, float rate);

		/// <summary>
		/// Получение списка месяцев типа <see cref="SheetMonth"/> для списка сотрудников <see cref="Employee"/>
		/// </summary>
		/// <param name="baseMonth">Базовый месяц, на основе которого создаются <see cref="SheetMonth"/></param>
		/// <param name="employees">Список сотрудников, для которых создаются <see cref="SheetMonth"/></param>
		/// <returns></returns>
		public IList<SheetMonth> MakeManyEmployeesMonths(Month baseMonth, WorkingWeekConfig weekConfig, IList<Employee> employees);

	}
}
