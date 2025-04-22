using Microsoft.AspNetCore.Mvc.Rendering;

namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class ReplaceRoleViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public SelectList? AvailableRoles { get; set; }
    }
}
