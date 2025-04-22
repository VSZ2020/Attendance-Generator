using Microsoft.AspNetCore.Mvc.Rendering;

namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class ReassignEmployeeViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public Guid? AssignedEmployeeId { get; set; }

        public SelectList? AvailableEmployees { get; set; }
    }
}
