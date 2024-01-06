using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.Entities
{
    public class ContractEntity: BaseEntity
    {
        /// <summary>
        /// Номер договора
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата создания договора
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
