using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Entities
{
    public class FunctionGroupEntity: BaseEntity
    {
        public string GroupName { get; set; }
        public IList<FunctionEntity> Functions { get; set; }

        public static IList<FunctionGroupEntity> GetDefault()
        {
            int id = 1;
            return new List<FunctionGroupEntity>()
            {
                new FunctionGroupEntity()
                {
                    Id = id++,
                    GroupName = "Административные должности"
                },
                new FunctionGroupEntity()
                {
                    Id = id++,
                    GroupName = "Научные должности"
                },
            };
        }
    }
}
