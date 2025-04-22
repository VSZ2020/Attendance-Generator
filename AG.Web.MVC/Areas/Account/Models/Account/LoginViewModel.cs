using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.Account.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Введите имя пользователя")]
        [DataType(DataType.Text)]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
