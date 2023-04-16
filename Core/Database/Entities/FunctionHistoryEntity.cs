using System;

namespace Core.Database.Entities
{
    public class FunctionHistoryEntity: BaseEntity
    {
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; }
        public int FunctionId { get; set; }
        public FunctionEntity Function { get; set; }

        /// <summary>
        /// Дата найма на должность
        /// </summary>
        public DateTime HiredAt { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? FiredAt { get; set; }
    }
}
