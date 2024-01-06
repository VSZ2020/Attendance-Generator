using System.Collections.Generic;

namespace Core.Database.Entities
{
	public class TimeIntervalTypeEntity: BaseEntity
    {
        public string Name { get; set; } = "Без названия";
        public string ShortName { get; set; } = "Б/Н";
        public string Description { get; set; } = "Описание отсутствует";

        public IList<TimeIntervalEntity>? TimeIntervals { get; set; }

        
    }
}
