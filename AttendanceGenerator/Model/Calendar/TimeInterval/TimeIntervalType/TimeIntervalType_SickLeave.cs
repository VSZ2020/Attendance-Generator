using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Больничный
    /// </summary>
    public class TimeIntervalType_SickLeave : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.SickLeave;
        public string Title => App.Current.Resources["IntervalType_Title_SickLeave"].ToString() ?? "[Больничный]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_SickLeave"].ToString() ?? "[Б]";

        public string Decription { get; set; } = string.Empty;
    }
}
