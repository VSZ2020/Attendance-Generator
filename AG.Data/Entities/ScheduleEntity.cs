using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class ScheduleEntity: BaseEntity
    {
        public string Title { get; set; }

        public ICollection<EmployeeToDepartment> EmployeeToDepartmentTable { get; set; }
        
        public ICollection<ScheduleDayEntity> Days { get; set; }
    }
}
