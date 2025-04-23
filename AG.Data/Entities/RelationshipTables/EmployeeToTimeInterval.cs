using System.ComponentModel.DataAnnotations;
using AG.Core.Enums;

namespace AG.Data.Entities.RelationshipTables
{
    public class EmployeeToTimeInterval : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public EmployeeEntity? Employee { get; set; }
        
        public DayType TimeIntervalType { get; set; }

        [Required]
        public DateTime Begin { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
