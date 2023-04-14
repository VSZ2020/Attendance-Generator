using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    public class TimeIntervalType_UserDefined : ITimeIntervalType
    {
        public string Title => String.Empty;

        public string ShortTitle => String.Empty;

        public string Decription { get; set; } = string.Empty;

        TimeIntervalTypes.ETimeIntervalType ITimeIntervalType.IntervalType => TimeIntervalTypes.ETimeIntervalType.UserDefined;
    }
}
