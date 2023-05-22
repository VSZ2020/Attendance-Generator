using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Calendar
{
	public interface ICalendar
	{
		/// <summary>
		/// Хранит выбранный год
		/// </summary>
		public int CurrentYear { get; set; }

		/// <summary>
		/// Хранит выбранный месяц
		/// </summary>
		public int CurrentMonth { get; set; }

		/// <summary>
		/// Хранит выбранный день
		/// </summary>
		public int CurrentDay { get; set; }

		public DateTime CurrentDate { get; }

		/// <summary>
		/// Хранит перечень дней
		/// </summary>
		public IList<Day> Days { get; }

		/// <summary>
		/// Список месяцев
		/// </summary>
		public IList<Month> Months { get; }
	}
}
