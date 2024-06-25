using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Data.Entities
{
    public class EstablishmentEntity: BaseEntity
    {
        [Required]
        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string INN { get; set; }

        public string OGRN { get; set; }

        public string Header { get; set; }

        public DateTime? RegistrationDate { get; set; }


        public ICollection<DepartmentEntity> Departments { get; set; }
    }
}
