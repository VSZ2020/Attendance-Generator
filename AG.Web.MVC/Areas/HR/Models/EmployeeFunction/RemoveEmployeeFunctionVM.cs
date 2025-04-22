using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class RemoveEmployeeFunctionVM
    {
        [Required]
        public Guid RecordId { get; set; }

        public string? DepartmentName { get; set; }
        public string? EmployeeName { get; set; }
        public string? FunctionName { get; set; }

        public Guid? EmployeeId { get; set; }
    }
}
