using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class TimeIntervalEntity: BaseEntity
    {
        public int IntervalTypeId { get; set; }
        public TimeIntervalTypeEntity? IntervalType { get; set; }

        public DateTime StartDate { get;set; }
        public DateTime EndDate { get;set; }

        public EmployeeEntity? Employee { get; set; }
        public int EmployeeId { get; set; }

        public static IList<TimeIntervalEntity> GetDefault()
        {
            throw new NotImplementedException();
        }
    }
}
