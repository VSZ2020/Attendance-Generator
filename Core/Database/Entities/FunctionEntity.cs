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
        public Guid FunctionGroupId { get; set; }

        /// <summary>
        /// Группа, к которой относится должность
        /// </summary>
        public FunctionGroupEntity? Group { get; set; }

        /// <summary>
        /// Перечень сотрудников с данной должностью
        /// </summary>
        public IList<EmployeeEntity>? Employees { get; set; }
    }
}
