namespace AG.Web.MVC.Models.Employee
{
    public record class IndexEmployeeVM
    {
        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public List<EmployeeVM> Employees { get; set; }
    }
}
