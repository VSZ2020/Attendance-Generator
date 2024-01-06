using System.Collections.Generic;

namespace Core.Database.Entities
{
	public class FunctionGroupEntity: BaseEntity
    {
        public string GroupName { get; set; }
        public IList<FunctionEntity>? Functions { get; set; }
    }
}
