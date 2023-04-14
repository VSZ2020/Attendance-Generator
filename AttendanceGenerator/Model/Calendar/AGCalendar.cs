using AttendanceGenerator.Model.Calendar.WorkingWeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar
{
    public class AGCalendar
    {
        /// <summary>
        /// Текущий год
        /// </summary>
        public int CurrentYear { get; }
        /// <summary>
        /// Текущий месяц
        /// </summary>
        public int CurrentMonth { get; }
        /// <summary>
        /// Обновление календаря в соответствии с текущей датой
        /// </summary>
        public void Update()
        {

        }


        /// <summary>
        /// Список доступных месяцев
        /// </summary>
        public List<Month> Months { get; }

        public List<DateOnly> Holidays { get; }

        public void LoadHolidaysFromFile(string filePath)
        {

        }
        public void LoadHolidays(Uri uri_address)
        {

        }
    }
}
