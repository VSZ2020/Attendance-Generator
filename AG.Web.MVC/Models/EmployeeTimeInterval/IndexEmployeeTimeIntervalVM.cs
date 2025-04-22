using Microsoft.AspNetCore.Mvc.Rendering;

namespace AG.Web.MVC.Models.EmployeeTimeInterval
{
    public class IndexEmployeeTimeIntervalVM
    {
        public Guid EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public IEnumerable<EmployeeTimeIntervalVM> TimeIntervals { get; set; } = default!;

        public int? FilterYear { get; set; }

        public int? FilterMonth { get; set; }

        public IEnumerable<SelectListItem>? AvailableMonths { get; set; }

        public Guid? RedirectDepartmentId { get; set; }
    }
}
