using System;
using AG.Core.Enums;

namespace AG.Core.Models
{
    public class EmployeeTimeInterval
    {
        public DayType Type { get; set; }
        
        public DateTime Begin { get; set; }
        
        public DateTime End { get; set; }
    }
}