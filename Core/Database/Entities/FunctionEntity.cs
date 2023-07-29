using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    /// <summary>
    /// Должность
    /// </summary>
    public class FunctionEntity: BaseEntity
    {
        /// <summary>
        /// Название должности
        /// </summary>
        public string Name { get; set; } = "Безымянная должность";

        /// <summary>
        /// Сокращенное название должности
        /// </summary>
        public string ShortName { get; set; } = "";

        /// <summary>
        /// Описание должности
        /// </summary>
        public string Description { get; set; } = "Нет описания должности";

        /// <summary>
        /// Идентификатор группы, к которой относится должность
        /// </summary>
        public int FunctionGroupId { get; set; }

        /// <summary>
        /// Группа, к которой относится должность
        /// </summary>
        public FunctionGroupEntity? Group { get; set; }

        /// <summary>
        /// Перечень сотрудников с данной должностью
        /// </summary>
        public IList<EmployeeEntity>? Employees { get; set; }

        public static IList<FunctionEntity> GetDefault()
        {
            int id = 1;
            int groupId = 1;
            return new List<FunctionEntity> { 
                //Административные должности
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Директор", ShortName = "дир."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Заместитель директора", ShortName = "зам.дир."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Исполняющий обязанности директора", ShortName = "и.о.дир."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Помощник директора", ShortName = "пом.дир."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Ученый секретарь", ShortName = "уч.сек."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Заведующий лабораторией", ShortName = "зав.лаб."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Заместитель заведующего лабораторией", ShortName = "зам.зав.лаб."},
                 new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Исполняющий обязанности заведующего лабораторией", ShortName = "и.о.зав.лаб."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Ведущий специалист по кадрам", ShortName = "вед.спец.кадр."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Специалист по кадрам", ShortName = "спец.кадр."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId++, Name = "Сотрудник охраны", ShortName = "сот.охр."},
                //Научные должности
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Инженер", ShortName = "инж."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Инженер-исследователь", ShortName = "инж.-иссл."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Младший научный сотрудник", ShortName = "м.н.с"},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Научный сотрудник", ShortName = "н.с."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Старший научный сотрудник", ShortName = "с.н.с."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId, Name = "Ведущий научный сотрудник", ShortName = "в.н.с."},
                new FunctionEntity(){ Id = id++, FunctionGroupId = groupId++, Name = "Главный научный сотрудник", ShortName = "г.н.с."},
            };
        }
    }
}
