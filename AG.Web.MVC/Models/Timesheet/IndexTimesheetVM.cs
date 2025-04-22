using Microsoft.AspNetCore.Mvc.Rendering;

namespace AG.Web.MVC.Models.Timesheet
{
    public class IndexTimesheetVM
    {
        public string? DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public int? FilterYear { get; set; }

        public int? FilterMonth { get; set; }

        public IEnumerable<TimesheetVM>? Timesheets { get; set; }

        public IEnumerable<SelectListItem>? AvaialbleMonths { get; set; }
    }
}
