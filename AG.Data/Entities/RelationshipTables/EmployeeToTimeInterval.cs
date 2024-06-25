using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities.RelationshipTables
{
    public class EmployeeToTimeInterval : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid TimeIntervalId { get; set; }

        [Required]
        public DateTime Begin { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
