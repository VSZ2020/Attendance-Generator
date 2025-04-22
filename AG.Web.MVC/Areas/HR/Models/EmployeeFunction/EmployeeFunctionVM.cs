namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class EmployeeFunctionVM
    {
        public Guid Id { get; set; }

        public Guid FuctionId { get; set; }

        public string FunctionName { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime? FiredDate { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsConcurrent { get; set; }

        public float Rate { get; set; }
    }
}
