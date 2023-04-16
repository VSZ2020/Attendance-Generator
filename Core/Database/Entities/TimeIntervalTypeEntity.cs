using System.Collections;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class TimeIntervalTypeEntity: BaseEntity
    {
        public string Name { get; set; } = "Без названия";
        public string ShortName { get; set; } = "Б/Н";
        public string Description { get; set; } = "Описание отсутствует";

        public IList<TimeIntervalEntity>? TimeIntervals { get; set; }

        public static IList<TimeIntervalTypeEntity> GetDefault()
        {
            int id = 1;
            return new List<TimeIntervalTypeEntity>()
            {
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Командировка", ShortName = "К", Description = ""},
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Отпуск", ShortName = "О", Description = ""},
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Отпуск без содержания", ShortName = "Б/С", Description = ""},
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Больничный", ShortName = "Б", Description = ""},
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Учебный отпуск", ShortName = "УО", Description = ""},
                new TimeIntervalTypeEntity(){ Id = id++, Name = "Прочие неявки", ShortName = "ПН", Description = ""},
            };
        }
    }
}
