namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string Role { get; set; } = string.Empty;

        public bool IsActivatedAccount { get; set; }

        public string Email { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        public string? AssigmedDepartment { get; set; } = null!;

        public string AssignedEmployee { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? LastVisit { get; set; }
    }
}
