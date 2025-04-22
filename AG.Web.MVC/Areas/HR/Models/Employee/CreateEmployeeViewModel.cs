using AG.Core.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.Employee
{
    public record class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Не указано подразделение сотрудника")]
        public Guid DepartmentId { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Не задана дата принятия на работу")]
        public DateTime AssignmentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Не указана должность сотрудника")]
        public Guid FunctionId { get; set; }

        public string? Reason { get; set; }

        [Required(ErrorMessage = "Не задана доля ставки")]
        [Range(0.125F, 1.0F, ErrorMessage = "Значение доли ставки должно быть в диапазоне от {0} до {1}")]
        public float Rate { get; set; }

        public bool IsConcurrent { get; set; }

        public SelectList? AvailableFunctions { get; set; }
        public SelectList? AvailableDepartments { get; set; }

    }
}
