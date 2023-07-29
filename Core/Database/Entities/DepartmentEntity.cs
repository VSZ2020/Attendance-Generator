using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class DepartmentEntity: BaseEntity
    {
        public string Name { get; set; } = "Без названия";
        public IList<EmployeeEntity>? Employees { get; set; }

        public EstablishmentEntity? Establishment { get; set; }
        public int EstablishmentId { get; set;}

        public List<FunctionEntity>? Functions { get; set; }

        public static IList<DepartmentEntity> GetDefault()
        {
            int id = 1;
            int establishmentId = 1;
            return new List<DepartmentEntity>()
            {
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Лаборатория урбанизированной среды",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Радиационная лаборатория",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Лаборатория математического моделирования",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Лаборатория физики и экологии",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Лаборатория эколого-климатических проблем Арктики",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "Химико-аналитический центр",
                    EstablishmentId = establishmentId,
                },
                new DepartmentEntity()
                {
                    Id = id++,
                    Name = "ЦКП Арктических исследований",
                    EstablishmentId = establishmentId,
                }
            };
        }
    }
}
