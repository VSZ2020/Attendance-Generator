using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Командировка
    /// </summary>
    public class TimeIntervalType_Business : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.Business;
        public string Title => App.Current.Resources["IntervalType_Title_Business"].ToString() ?? "[Командировка]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_Business"].ToString() ?? "[К]";

        public string Decription { get; set; } = string.Empty;
    }
}
