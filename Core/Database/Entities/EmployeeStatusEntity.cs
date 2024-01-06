using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class EmployeeStatusEntity: BaseEntity
    {
        public string Name { get; set; }
        public IList<EmployeeEntity>? Employees { get; set; }
    }
}
