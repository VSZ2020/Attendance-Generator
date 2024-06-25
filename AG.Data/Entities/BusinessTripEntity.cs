using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class BusinessTripEntity: BaseEntity
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public string Reason { get; set; }

        public string Target { get; set; }

        public string Comment { get; set; }



        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmployeeEntity? Employee { get; set; }
    }
}
