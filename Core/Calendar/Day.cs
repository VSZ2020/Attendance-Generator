using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Calendar
{
    public class Day
    {
        private DateTime Date;
        public int Month => Date.Month;
        public int Year => Date.Year;

        /// <summary>
        /// Задает тип дня: выходной, праздничный, предпраздничный
        /// </summary>
        public DayType Type { get; private set; }

        public Day(int day, int month, int year, DayType type)
        {
            this.Date = new DateTime(year, month, day);
            this.Type = type;
            
        }
    }
}
