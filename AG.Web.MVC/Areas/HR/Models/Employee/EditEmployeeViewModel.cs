using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.Employee
{
    [BindRequired]
    public record class EditEmployeeViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}
