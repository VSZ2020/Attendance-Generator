using AG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Web.MVC.Models.Timesheet
{
    public class TimesheetVM
    {
        public Guid Id { get; set; }

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
        public string? FormType { get; set; }

        public bool HasContent { get; set; }

        /// <summary>
        /// Primary, corrective, etc. 
        /// Default 0 - Primary
        /// </summary>
        public int Kind { get; set; } = 0;

        public string? AuthorName { get; set; }

        public string? ExecutorName { get; set; }
        public string? ResponsibleExecutorName { get; set; }
        public string? AccountingExecutorName { get; set; }
        public string? DepartmentHeaderName { get; set; }
    }
}
