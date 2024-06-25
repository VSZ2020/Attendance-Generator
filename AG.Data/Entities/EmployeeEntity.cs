using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class EmployeeEntity: BaseEntity
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int SheetNumber { get; set; }

        public int Status { get; set; }

        public ICollection<BusinessTripEntity> BusinessTrips { get; set; }
        public ICollection<ContractEntity> Contracts { get; set; }
        public ICollection<EmployeeToDepartment> EmployeeToDepartmentTable { get; set;}
        public ICollection<EmployeeToFunction> EmployeeToFunctionTable { get; set; }
        public ICollection<EmployeeToTimeInterval> EmployeeToTimeIntervalTable { get; set; }
    }
}
