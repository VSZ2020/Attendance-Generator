﻿using Core.Calendar;
using Services.Infrastructure.Configuration.Configs;
using Services.Extensions;
using Services.Factories;
using System.Linq;
using SQLiteRepository;
using Services.Database;
using System;
using Services.Domains;
using Services.Domains.ReportCard;

namespace Services.Calendar
{
	public class CalendarService: ICalendarService
	{
		#region ctor
		public CalendarService(IEmployeeService employeeService, ICalendar? calendar = null) 
		{ 
			this.employeeService = employeeService;  
			this.calendar = calendar; 
		}
		#endregion ctor

		#region fields
		private readonly ICalendar? calendar;
		private readonly IEmployeeService employeeService;
		
		#endregion fields

		public int GetHolidaysCount()
		{
			return GetHolidaysCount(this.calendar);
		}

		public int GetHolidaysCount(ICalendar? calendar)
		{
			return calendar?.Days?.Where(d => d.Type == DayType.Holiday).Count() ?? 0;
		}

		public int GetHolidaysCount(IList<Day>? days)
		{
			return days?.Where(d => d.Type == DayType.Holiday).Count() ?? 0;
		}

		public Month CreateMonth(
			DateTime date,
			WorkingWeekConfig? weekConfig,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null)
		{
			return CreateMonth(date.Year, date.Month, weekConfig.DayTypes, holidays, userDaysCorrections);
		}

		public Month CreateMonth(
			DateTime date,
			Dictionary<DayOfWeek, DayType> weekDayTypes,
			IList<DateTime>? holidays = null,
			IList<Day>? userDaysCorrections = null)
		{
			return CreateMonth(date.Year, date.Month, weekDayTypes, holidays, userDaysCorrections);
		}

		public Month CreateMonth(
			int year, int month, 
			WorkingWeekConfig? weekConfig, 
			IList<DateTime>? holidays = null, 
			IList<Day>? userDaysCorrections = null)
		{
			if (weekConfig == null)
				throw new ArgumentNullException(nameof(WorkingWeekConfig));
			return CreateMonth(year, month, weekConfig.DayTypes, holidays, userDaysCorrections);
		}

		
		public Month CreateMonth(
			int year, int month, 
			Dictionary<DayOfWeek, DayType> weekDayTypes, 
			IList<DateTime>? holidays = null, 
			IList<Day>? userDaysCorrections = null) 
		{
			if (weekDayTypes == null)
				throw new ArgumentNullException(nameof(weekDayTypes));

			int daysInMonth = DateTime.DaysInMonth(year, month);
			Month newMonth = CalendarFactory.CreateMonth(year, month);
			for (int i = 1; i <= daysInMonth; i++)
			{
				var curDate = new DateTime(year, month, i);
				var dayofWeek = curDate.DayOfWeek;
				var dayType = weekDayTypes[dayofWeek];
				var day = CalendarFactory.CreateDay(curDate, dayType);
				newMonth.AddDay(day);
			}
            
			return newMonth.MakeMonthHolidaysCorrection(holidays).MakeUserDaysCorrections(userDaysCorrections);
		}
		
		public SheetMonth MakeEmployeeMonth(Month currentMonth, WorkingWeekConfig weekConfig, Employee employee)
		{
			var response = employeeService.GetEmployeePeriods(employee);
			var timeIntervals = response.StatusCode == DatabaseResponse<TimeInterval>.ResponseCode.Success && response.Results != null ? response.Results : new List<TimeInterval>();
			return MakeEmployeeMonth(currentMonth, weekConfig, timeIntervals, employee.Rate);
		}

		
		public SheetMonth MakeEmployeeMonth(Month currentMonth, WorkingWeekConfig weekConfig, IList<TimeInterval> employeePeriods, float rate)
		{
			var newMonth = new SheetMonth(currentMonth.Year, currentMonth.Id);
			for (int i = 0; i < currentMonth.DaysCount; i++)
			{
				var day = currentMonth[i];
				var interval = employeePeriods.InsideInterval(day.Date);
				var newDay = new SheetDay(day.Year, day.Month, day.DayNumber, day.Type, interval?.TimeIntervalType ?? null);
				newDay.WorkingHours = 
					interval != null ? 
					GetCustomTimeIntervalHours(interval.TimeIntervalType, newDay.DayOfWeek, weekConfig) : 
					GetHoursForDayType(newDay.DayType, newDay.DayOfWeek, weekConfig);
				//Учитываем долю ставки сотрудника при расчете рабочих часов
				newDay.WorkingHours *= rate;
				newMonth.AddDay(newDay);
			}
			return newMonth;
		}

		public IList<SheetMonth> MakeManyEmployeesMonths(Month baseMonth, WorkingWeekConfig weekConfig, IList<Employee> employees)
		{
			return employees.Select(empl => MakeEmployeeMonth(baseMonth, weekConfig, empl)).ToList();
		}

		#region Utils
		/// <summary>
		/// Возвращает список праздников в году. Если <see cref="ICalendar"/> NULL, то используется текущий год
		/// </summary>
		/// <param name="calendar">Объект календаря</param>
		/// <returns>Список праздничных дней</returns>
		public static IList<DateTime> GetDefaultHolidays(ICalendar? calendar = null)
		{
			int curYear = calendar?.CurrentYear ?? DateTime.Now.Year;

			return new List<DateTime>()
			{
				//Праздники в январе
				new DateTime(curYear, 1, 1),
				new DateTime(curYear, 1, 2),
				new DateTime(curYear, 1, 3),
				new DateTime(curYear, 1, 4),
				new DateTime(curYear, 1, 5),
				new DateTime(curYear, 1, 6),
				new DateTime(curYear, 1, 7),

				//Праздники в феврале
				new DateTime(curYear, 2, 23),

				//Праздники в марте
				new DateTime(curYear, 3, 8),

				//Праздники в мае
				new DateTime(curYear, 5, 1),
				new DateTime(curYear, 5, 9),
				
				//Праздники в июне
				new DateTime(curYear, 6, 12),

				//Праздники в ноябре
				new DateTime(curYear, 11, 4)
			};
		}

		public float GetHoursForDayType(DayType dayType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig)
		{
			return dayType switch
			{
				DayType.DayOff | DayType.Holiday => 0,
				DayType.PreHoliday => weekConfig.HoursInShortDay,
				DayType.Working => weekConfig.WorkingHours[dayOfWeek],
				_ => 0
			};
		}

		/// <summary>
		/// Содержит правила обработки для пользовательских временных интервалов
		/// </summary>
		/// <param name="intervalType">Тип пользовательского временнОго интервала</param>
		/// <param name="dayOfWeek">День недели</param>
		/// <param name="weekConfig">Конфигурация для недели</param>
		/// <returns>Количество часов, соотвествующих пользовательскому типу временного интервала</returns>
		public float GetCustomTimeIntervalHours(TimeIntervalType? intervalType, DayOfWeek dayOfWeek, WorkingWeekConfig weekConfig)
		{
			if (intervalType == null)
				return 0;
			return intervalType.ShortName switch
			{
				"К" => weekConfig.WorkingHours[dayOfWeek],
				_ => 0f
			};
		}
		#endregion
	}
}