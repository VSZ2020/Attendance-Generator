using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.Establishment
{
    public record class EstablishmentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Организация должна иметь название")]
        [BindRequired]
        public string Title { get; set; }

        public string ShortTitle { get; set; }

        public string? Header { get; set; }
        public string? HeaderFunction { get; set; }

        [BindRequired]
        [Required(ErrorMessage = "Для организации должна быть указана дата регистрации")]
        public DateTime? RegistrationDate { get; set; }

        [BindRequired]
        [Length(10, 10, ErrorMessage = "ИНН должен содержать 10 цифр")]
        [Required(ErrorMessage = "Поле ИНН обязательно для заполнения")]
        public string INN { get; set; }

        [BindRequired]
        [Length(13, 13, ErrorMessage = "ОГРН должен содержать 13 цифр")]
        [Required(ErrorMessage = "Поле ОГРН обязательно для заполнения")]
        public string OGRN { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phones { get; set; }
    }
}
