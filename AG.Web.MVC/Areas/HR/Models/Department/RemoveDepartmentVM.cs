using AG.Web.MVC.Areas.HR.Models.EmployeeFunction;

namespace AG.Web.MVC.Areas.HR.Models.Department
{
    public class RemoveDepartmentVM
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public List<EmployeeFunctionToRemoveVM>? FunctionsToRemove { get; set; } = null!;
    }
}
