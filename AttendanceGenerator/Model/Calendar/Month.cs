using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar
{
    public class Month
    {
        /// <summary>
        /// Индекс месяца (от 1 до 12)
        /// </summary>
        public int MonthIndex { get; }

        /// <summary>
        /// Год, к которому принадлежит месяц
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Возвращает день по его номеру (от 1 до 30-31)
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public Day this[int index] {get => Days[index];}

        /// <summary>
        /// Возвращает количество дней в месяце
        /// </summary>
        public int DaysCount { get; } = 0;

        /// <summary>
        /// Список дней
        /// </summary>
        List<Day> Days { get; } = new List<Day>();

        /// <summary>
        /// Название месяца
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Возвращает количество нерабочих дней
        /// </summary>
        public int HolidaysCount { get; }
    }
}
