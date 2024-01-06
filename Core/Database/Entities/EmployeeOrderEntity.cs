using System;

namespace Core.Database.Entities
{
    public class EmployeeOrderEntity: BaseEntity
    {
        /// <summary>
        /// Номер приказа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата подписания приказа
        /// </summary>
        public DateTime SigningDate { get; set; }

        /// <summary>
        /// Тип приказа (о приеме на работу, о переводе, об увольнении)
        /// </summary>
        public Guid OrderType { get; set; }
    }

    /// <summary>
    /// Тип приказа на сотрудника
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Прием на работу
        /// </summary>
        Hiring = 0,

        /// <summary>
        /// Перевод в другой отдел
        /// </summary>
        Transfer = 1,

        /// <summary>
        /// Увольнение
        /// </summary>
        Dismissal = 2
    }
}
