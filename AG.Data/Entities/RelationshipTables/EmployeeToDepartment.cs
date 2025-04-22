using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities.RelationshipTables
{
    public class EmployeeToDepartment : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public EmployeeEntity? Employee { get; set; }

        public Guid DepartmentId { get; set; }
        public DepartmentEntity? Department  { get; set; }

        public Guid FunctionId { get; set; }
        public FunctionEntity? Function { get; set; }

        public Guid ScheduleId { get; set; }
        public ScheduleEntity? Schedule { get; set; }

        [Required]
        public DateTime AssignmentDate { get; set; }

        public DateTime? FiredDate { get; set; }

        public string? Reason { get; set; }

        [Required]
        public float Rate { get; set; }

        [Required]
        public bool IsConcurrent { get; set; }

        public int TimesheetNumber { get; set; }
    }
}
