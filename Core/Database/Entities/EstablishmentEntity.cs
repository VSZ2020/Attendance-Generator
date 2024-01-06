using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class EstablishmentEntity: BaseEntity
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; } = "Организация без названия";

        /// <summary>
        /// Краткое название организации
        /// </summary>
        public string? ShortName { get; set; }

        /// <summary>
        /// ОГРН организации
        /// </summary>
        public string? OGRN { get; set; }
        /// <summary>
        /// ИНН организации
        /// </summary>
        public string? INN { get; set; }
        public string? RegistrationDate { get; set; }

        /// <summary>
        /// Юридический адрес организации
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Электронный адрес почты организации
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Номера телефонов организации
        /// </summary>
        public string? Phones { get; set; }

        /// <summary>
        /// Руководитель организации
        /// </summary>
        public string? Header { get; set; }

        public IList<DepartmentEntity>? Departments { get; set; }

        public IList<CorrectionDayEntity>? CorrectionDays { get; set;}
    }
}
