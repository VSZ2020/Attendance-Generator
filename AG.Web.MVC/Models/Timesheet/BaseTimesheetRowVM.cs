namespace AG.Web.MVC.Models.Timesheet;

public abstract class BaseTimesheetRowVM
{
    public int Number { get; set; } = 0;

    /// <summary>
    /// Last, first and middle name of employee
    /// </summary>
    public string EmployeeName { get; set; } = string.Empty;

    /// <summary>
    /// Employee position
    /// </summary>
    public string Function { get; set; } = string.Empty;

    /// <summary>
    /// Employee rate between 0,125 and 1,0
    /// </summary>
    public float Rate { get; set; }
    
    /// <summary>
    /// Employee status
    /// </summary>
    public bool IsConcurrent { get; set; }
        
    public List<DayVM> Days { get; set; }
}