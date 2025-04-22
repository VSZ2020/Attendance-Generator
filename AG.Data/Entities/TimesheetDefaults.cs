using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class TimesheetDefaults: BaseEntity
    {
        public string? ResponsibleExecutor { get; set; }
        public string? ResponsibleExecutorFunction { get; set; }

        public string? Executor { get; set; }
        public string? ExecutorFunction { get; set; }

        public string? AccountingExecutor { get; set; }
        public string? AccountingExecutorFunction { get; set; }



        public Guid DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DepartmentEntity? Department { get; set; }
    }
}
