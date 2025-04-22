namespace AG.Web.MVC.Areas.HR.Models.Employee
{
    public record class EmployeeIndexViewModel
    {
        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public List<EmployeeViewModel> Employees { get; set; }

        public string? FilterName { get; set; }
    }
}
