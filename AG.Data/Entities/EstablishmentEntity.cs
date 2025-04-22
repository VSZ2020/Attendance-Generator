using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class EstablishmentEntity: BaseEntity
    {
        [Required]
        public string FullName { get; set; }

        public string ShortName { get; set; }

        public string INN { get; set; }

        public string OGRN { get; set; }

        public string? Header { get; set; }

        public string? HeaderFunction { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Phones { get; set; }

        public DateTime? RegistrationDate { get; set; }


        public List<ScheduleEntity>? Schedules { get; set; }
        public ICollection<DepartmentEntity> Departments { get; set; }
    }
}
