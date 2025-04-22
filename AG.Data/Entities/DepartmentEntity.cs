using AG.Data.Entities.RelationshipTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class DepartmentEntity: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string? Header { get; set; }


        #region Establishment
        public Guid EstablishmentId { get; set; }

        [ForeignKey(nameof(EstablishmentId))]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public EstablishmentEntity? Establishment { get; set; }
        #endregion


        #region TimesheetsDefaults
        public TimesheetDefaults? TimesheetsDefaults { get; set; } 
        #endregion


        public ICollection<TimesheetEntity> Timesheets { get; set; }
        //public ICollection<EmployeeEntity> Employees { get; set; }
        public ICollection<EmployeeToDepartment> EmployeeToDepartmentTable { get; set; }
    }
}
