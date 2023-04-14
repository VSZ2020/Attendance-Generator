using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Прочие неявки
    /// </summary>
    public class TimeIntervalType_Other : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.Other;
        public string Title => App.Current.Resources["IntervalType_Title_Other"].ToString() ?? "[Прочие неявки]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_Other"].ToString() ?? "[ПН]";

        public string Decription { get; set; } = string.Empty;
    }
}
