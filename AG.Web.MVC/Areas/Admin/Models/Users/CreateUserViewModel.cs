using AG.Core.Policy;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class CreateUserViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Не указан логин")]
        public string Username { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не выбрана роль пользователя в системе")]
        public string Role { get; set; } = DefaultRoles.EMPLOYEE;

        public bool IsActivatedAccount { get; set; } = true;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        public Guid? AssignedDepartmentId { get; set; }

        public Guid? AssignedEmployeeId { get; set; }

        public SelectList? AvailableRoles { get; set; }

        public SelectList? AvailableEmployees { get; set; }

        public SelectList? AvailableDepartmetns { get; set; }
    }
}
