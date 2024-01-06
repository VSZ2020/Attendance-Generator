using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class DepartmentEntity: BaseEntity
    {
        public string Name { get; set; } = "Без названия";
        public IList<EmployeeEntity>? Employees { get; set; }

        public EstablishmentEntity? Establishment { get; set; }
        public Guid EstablishmentId { get; set;}

        public Guid HeadOfLabId { get; set; }
    }
}
