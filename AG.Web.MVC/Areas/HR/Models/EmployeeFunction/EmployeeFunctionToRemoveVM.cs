namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class EmployeeFunctionToRemoveVM
    {
        public string EmployeeName { get; set; }
        public string FunctionName { get; set; }
        public string DepartmentName { get; set; }

        public float Rate { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime? FiredDate { get; set; }

        public bool IsConcurrent { get; set; }
    }
}
