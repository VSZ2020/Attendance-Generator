using System;
using System.Collections.Generic;

namespace Core.Database.Entities
{
    public class EstablishmentEntity: BaseEntity
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; } = "Безымянная организация";

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
        public EmployeeEntity? Header { get; set; }

        public IList<DepartmentEntity>? Departments { get; set; }

        public static IList<EstablishmentEntity> GetDefault()
        {
            return new List<EstablishmentEntity>
            {
                new EstablishmentEntity(){
                    Id = 1,
                    Name = "Институт промышленной экологии ИПЭ УрО РАН",
                    ShortName = "ИПЭ УрО РАН",
                    Address = "620108, Свердловская область, город Екатеринбург, Софьи Ковалевской ул., д.20",
                    INN = "6660001481", OGRN = "1026604959370", RegistrationDate = "19.12.1992"}
            };
        }
    }
}
