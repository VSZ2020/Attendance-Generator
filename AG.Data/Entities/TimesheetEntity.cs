using AG.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class TimesheetEntity: BaseEntity
    {
        /// <summary>
        /// Beginning of reporting period
        /// </summary>
        [Required]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// End of reporting period
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }

        /// <summary>
        /// Timesheet form type: T-12, T-13 or 0504421
        /// </summary>
        public TimesheetFormType? FormType { get; set; }

        /// <summary>
        /// Primary, corrective, etc. 
        /// Default 0 - Primary
        /// </summary>
        public int Kind { get; set; } = 0;

        public string? JsonContent { get; set; }

        public string? Comment { get; set; }


        public Guid? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public UserEntity? Author { get; set; }


        public Guid DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DepartmentEntity? Department { get; set; }


        public string? ResponsibleExecutor { get; set; }
        public string? ResponsibleExecutorFunction { get; set; }
        public bool? IsSignedByResponsibleExecutor { get; set; }

        public string? Executor { get; set; }
        public string? ExecutorFunction { get; set; }
        public DateTime? ExecutorSignedAt { get; set; }
        public bool? IsSignedByExecutor { get; set; }

        public string? AccountingExecutor { get; set; }
        public string? AccountingExecutorFunction { get; set; }
        public DateTime? AccountingExecutorSignedAt { get; set; }
        public bool? IsSignedByAccountingExecutor { get; set; }

        public string? DepartmentHeader { get; set; }
        public string? DepartmentHeaderFunction { get; set; }
        public DateTime? DepartmentHeaderSignedAt { get; set; }
        public bool? IsSignedByHeader { get; set; }
    }
}
