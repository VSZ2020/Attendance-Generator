namespace Core.Database.Entities.JoinEntities
{
    /// <summary>
    /// Сущность, связывающая сотрудника, подразделение и должность
    /// </summary>
    public class EmployeeFunctionDepartmentEntity
    {
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public int FunctionId { get; set; }
    }
}
