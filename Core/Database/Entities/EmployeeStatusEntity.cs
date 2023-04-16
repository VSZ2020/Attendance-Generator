using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Entities
{
    public class EmployeeStatusEntity: BaseEntity
    {
        public string Name { get; set; }
        public IList<EmployeeEntity>? Employees { get; set; }

        public static IList<EmployeeStatusEntity> GetDefault()
        {
            int id = 1;
            return new List<EmployeeStatusEntity>()
            {
                new EmployeeStatusEntity(){ Id = id++, Name = "Активный"},
                new EmployeeStatusEntity(){ Id = id++, Name = "Уволен"},
            };
        }
    }
}
