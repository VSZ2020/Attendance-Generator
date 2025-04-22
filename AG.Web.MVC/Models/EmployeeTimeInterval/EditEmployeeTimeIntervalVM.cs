using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.EmployeeTimeInterval
{
    public class EditEmployeeTimeIntervalVM: BaseEmployeeTimeIntervalVM
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public string? TimeIntervalTitle { get; set; }
    }
}
