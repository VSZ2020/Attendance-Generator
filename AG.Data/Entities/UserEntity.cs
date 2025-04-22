using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class UserEntity: BaseEntity
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        public string? Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string Role { get; set; } = default!;

        public bool IsActivatedAccount { get; set; }

        public DateTime? LastVisit { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity? Employee { get; set; }

        public Guid? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public DepartmentEntity? Department { get; set; }

        public ICollection<TimesheetEntity> Timesheets { get; set; } = null!;
    }
}
