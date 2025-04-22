namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class EmployeeFunctionIndexVM
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public IEnumerable<EmployeeFunctionVM> EmployeeFunctions { get; set; }
    }
}
