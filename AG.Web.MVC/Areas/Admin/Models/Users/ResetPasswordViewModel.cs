using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.Admin.Models.Users
{
    public class ResetPasswordViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
