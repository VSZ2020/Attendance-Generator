using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Отпуск без содержания
    /// </summary>
    public class TimeIntervalType_UnpaidVacation : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.UnpaidVacation;
        public string Title => App.Current.Resources["IntervalType_Title_UnpaidVacation"].ToString() ?? "[Отпуск без содержания]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_UnpaidVacation"].ToString() ?? "[Б/С]";

        public string Decription { get; set; } = string.Empty;
    }
}
