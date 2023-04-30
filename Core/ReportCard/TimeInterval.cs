using System;

namespace Core.Sheet
{
    public class TimeInterval
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration => EndDate - StartDate;
    }
}
