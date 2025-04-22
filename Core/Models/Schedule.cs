using System;
using System.Collections.Generic;

namespace AG.Core.Models
{
    public struct ScheduleDay
    {
        public long WorkBegin;
        public long WorkEnd;

        public long BreakBegin;
        public long BreakEnd;

        public float WorkingTime;

        public DayOfWeek DayOfWeek;

        public bool IsDayOff;
    }

    public class Schedule
    {
        public string Title { get; set; } = string.Empty;

        public List<ScheduleDay> Days = new List<ScheduleDay>(7);
    }
}
