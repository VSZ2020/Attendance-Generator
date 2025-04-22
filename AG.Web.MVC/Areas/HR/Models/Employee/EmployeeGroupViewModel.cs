namespace AG.Web.MVC.Areas.HR.Models.Employee
{
    public class EmployeeGroupViewModel
    {
        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }
}
