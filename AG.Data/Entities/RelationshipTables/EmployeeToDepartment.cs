namespace AG.Data.Entities.RelationshipTables
{
    public class EmployeeToDepartment : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid DepartmentId { get; set; }


    }
}
