using AttendanceGenerator.Model.Calendar.DayType;
using System;

namespace AttendanceGenerator.Model.Calendar
{
    public class Day
    {
        public float Hours { get; set; }
        public DayTypes.EDayType DayType { get; set; }
        public DayOfWeek DayOfWeek { get; init; }
        public bool IsDayOff { get; }

        public Day(DayTypes.EDayType dayType, DayOfWeek DoW, bool isDayOff, float value)
        {
            this.DayType = dayType;
            this.Hours = value;
            this.DayOfWeek = DoW;
            this.IsDayOff = isDayOff;
        }
    }
}
