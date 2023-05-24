using System;

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

        public string? Comment { get; set; }

        
    }
}
