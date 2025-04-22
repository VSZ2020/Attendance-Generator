namespace AG.Web.MVC.Models.EmployeeTimeInterval;

public class RemoveEmployeeTimeIntervalVM
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public string IntervalName { get; set; }
    
    public DateTime Begin { get; set; }
    
    public DateTime End { get; set; }
}