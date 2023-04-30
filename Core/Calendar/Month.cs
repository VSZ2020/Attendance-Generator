using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Calendar
{
    public class Month
    {
        public int Year { get; set; }
        public IList<Day> Days { get; set; }

        public int Id { get; private set; }

        /// <summary>
        /// Количество рабочих дней в месяце
        /// </summary>
        public int WorkingDays => Days?.Where(d => d.Type == DayType.Working || d.Type == DayType.PreHoliday).Count() ?? 0;

        public Month(int monthId = 0, int year = 0)
        {
            this.Id = monthId == 0 ? DateTime.Now.Month : monthId;
            this.Year = year == 0 ? DateTime.Now.Year : year;
        }
    }
}
