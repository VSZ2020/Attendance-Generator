using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.WorkingWeek
{
    public class Week
    {
        public enum RussianDayOfWeek
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }
        public static RussianDayOfWeek GetRussianDayOfWeek(DayOfWeek DOW)
        {
            return DOW switch
            {
                DayOfWeek.Monday => RussianDayOfWeek.Monday,
                DayOfWeek.Tuesday => RussianDayOfWeek.Tuesday,
                DayOfWeek.Wednesday => RussianDayOfWeek.Wednesday,
                DayOfWeek.Thursday => RussianDayOfWeek.Thursday,
                DayOfWeek.Friday => RussianDayOfWeek.Friday,
                DayOfWeek.Saturday => RussianDayOfWeek.Saturday,
                DayOfWeek.Sunday => RussianDayOfWeek.Sunday
            };
        }
    }
}
