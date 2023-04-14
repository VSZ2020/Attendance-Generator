using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AttendanceGenerator.Model.Calendar.DayType.DayTypes;

namespace AttendanceGenerator.Model.Calendar.DayType
{
    public interface IDayType
    {
        public EDayType DType { get; }
        public string Title { get;}
        public string ShortTitle { get; }
        public string Description { get; set; }

        public bool IsDayOff { get; set; }
    }
}
