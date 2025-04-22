namespace AG.Web.MVC.Models.Timesheet
{
    public abstract class BaseTimesheetContentVM
    {
        public Guid Id { get; set; }
        public List<DateTime> Dates { get; set; }

        public DateTime Begin { get; set; }
        
        public DateTime End { get; set; }

        public string TimesheetKind { get; set; } = string.Empty;
        
        public string TimesheetNumber { get; set; }


        public string? ResponsibleEmployee { get; set; }

        public string? ResponsibleEmployeeFunction { get; set; }

        public string? ExecutiveEmployee { get; set; }

        public string? ExecutiveEmployeeFunction { get; set;}

        public string? AccountingEmployee { get; set; }
        
        public string? AccountingEmployeeFunction { get; set; }
    }
}
