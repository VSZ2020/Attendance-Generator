using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.Timesheet
{
    public class DefaultsTimesheetVM
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public string? ResponsibleExecutorName { get; set; }
        public string? ResponsibleExecutorFunction { get; set; }

        public string? ExecutorName { get; set; }
        public string? ExecutorFunction { get; set; }

        public string? AccountingExecutorName { get; set; }
        public string? AccountingExecutorFunction { get; set; }
    }
}
