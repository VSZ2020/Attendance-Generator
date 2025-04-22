using AG.Data.Entities.RelationshipTables;
using System.ComponentModel.DataAnnotations;

namespace AG.Data.Entities
{
    public class EmployeeEntity: BaseEntity
    {
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Guid? UserId { get; set; }
        public UserEntity? User { get; set; }

        public ICollection<BusinessTripEntity> BusinessTrips { get; set; } = null!;
        //public ICollection<ContractEntity> Contracts { get; set; }
        //public ICollection<DepartmentEntity> Departments { get; set; }
        //public ICollection<FunctionEntity> Functions { get; set; }
        public ICollection<TimeIntervalEntity> TimeIntervals { get; set; } = null!;


        public ICollection<EmployeeToDepartment> EmployeeToDepartmentTable { get; set; } = null!;
        // public ICollection<EmployeeToFunction> EmployeeToFunctionTable { get; set; }
        public ICollection<EmployeeToTimeInterval> EmployeeToTimeIntervalTable { get; set; } = null!;
    }
}
