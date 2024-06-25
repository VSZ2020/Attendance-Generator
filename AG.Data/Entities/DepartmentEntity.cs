using AG.Data.Entities.RelationshipTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class DepartmentEntity: BaseEntity
    {
        public string Name { get; set; }

        public string Header { get; set; }

        
        public Guid EstablishmentId { get; set; }

        [ForeignKey(nameof(EstablishmentId))]
        [DeleteBehavior(DeleteBehavior.SetNull)]
        public EstablishmentEntity? Establishment { get; set; }


        public ICollection<EmployeeToDepartment> EmployeeToDepartmentTable { get; set; }
    }
}
