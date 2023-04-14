using AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration
{
    public static class StandartWeekHours
    {
        /// <summary>
        /// Стандартные длительности рабочей недели
        /// </summary>
        public static List<float> StandartHours = new List<float>()
        {
            8.25F, 8.25F, 8.25F, 8.25F, 7.6F, 0F, 0F
        };
        public static string ToHoursString(this List<float> hoursMatrix)
        {
            string hoursString = "";
            foreach (var hour in hoursMatrix)
                hoursString += hour.ToString() + ";";
            return hoursString.TrimEnd(';');
        }
    }
}
