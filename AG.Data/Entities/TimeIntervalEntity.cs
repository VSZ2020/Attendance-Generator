using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations;

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



        public ICollection<EmployeeToTimeInterval> EmployeeToTimeIntervalTable { get; set; }
    }
}
