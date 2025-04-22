using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.Home
{
    public class FeedbackVM
    {
        [Required(ErrorMessage = "Укажите ваше имя")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Укажите адрес электронной почты для связи")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите текст вашего сообщения, иначе обратная связь бессмысленна")]
        public string Message { get; set; }
    }
}
