using AG.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Data.Entities
{
    public class ContractEntity: BaseEntity
    {
        public string Number { get; set; }

        public ContractType ContractType { get; set; }

        public DateOnly SubmissionDate { get; set; }



        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public EmployeeEntity? Employee { get; set; }
    }
}
