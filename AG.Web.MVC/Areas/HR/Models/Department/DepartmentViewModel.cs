using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.Department
{
    public record class DepartmentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не задано название подразделения")]
        [RegularExpression(@"[А-я0-9]+(\s[А-я0-9]+)", ErrorMessage = "Название подразделения должно состоять только из букв или цифр")]
        public string Name { get; set; }

        //[RegularExpression(@"([А-я]+ [А-Я]\.\s[А-Я]\.){0,1}", ErrorMessage = "Неверный формат записи ФИО руководителя подразделения. Запись должна быть в следующем виде:  Фамилия И. О.")]
        public string? Header { get; set; }

        public Guid EstablishmentId { get; set; }

        [BindingBehavior(BindingBehavior.Never)]
        public IEnumerable<string>? HeadersList { get; set; }

        public int EmployeesCount { get; set; }
    }
}
