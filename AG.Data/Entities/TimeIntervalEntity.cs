using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations;
using AG.Core.Enums;

namespace AG.Data.Entities
{
    public class TimeIntervalEntity: BaseEntity
    {
        [Required]
        public int CODE { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortTitle { get; set; }

        public string? Reason { get; set; }

        [Required]
        public DayType DayType { get; set; }
        
        public ICollection<EmployeeEntity> Employees { get; set; }

        public ICollection<EmployeeToTimeInterval> EmployeeToTimeIntervalTable { get; set; }
    }
}
