using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Отпуск
    /// </summary>
    public class TimeIntervalType_Vacation : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.Vacation;
        public string Title => App.Current.Resources["IntervalType_Title_Vacation"].ToString() ?? "[Отпуск]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_Vacation"].ToString() ?? "[О]";

        public string Decription { get; set; } = string.Empty;
    }
}
