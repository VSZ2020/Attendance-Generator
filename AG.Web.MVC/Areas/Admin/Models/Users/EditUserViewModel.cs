using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public bool IsActivatedAccount { get; set; } = true;

        //[Required(AllowEmptyStrings = true, ErrorMessage = "Не указан e-mail")]
        //[DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        public Guid? AssignedDepartmentId { get; set; }

        public Guid? AssignedEmployeeId { get; set; }

        public SelectList? AvailableRoles { get; set; }

        public SelectList? AvailableEmployees { get; set; }

        public SelectList? AvailableDepartmetns { get; set; }
    }
}
