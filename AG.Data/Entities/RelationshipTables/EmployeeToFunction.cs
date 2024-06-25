using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities.RelationshipTables
{
    public class EmployeeToFunction : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid FunctionId { get; set; }

        public DateTime AssignmentDate { get; set; }

        public string Reason { get; set; }

        [Required]
        public float Rate { get; set; }
    }
}
