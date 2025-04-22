namespace AG.Web.MVC.Models.EmployeeTimeInterval
{
    public class EmployeeTimeIntervalVM: BaseEmployeeTimeIntervalVM
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? ShortTitle { get; set; }
    }
}
