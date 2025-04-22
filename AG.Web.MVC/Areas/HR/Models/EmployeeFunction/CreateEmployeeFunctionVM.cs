using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class CreateEmployeeFunctionVM
    {
        public Guid EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Не указана дата вступления в должность")]
        public DateTime AssignmentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FiredDate { get; set; }

        [Required(ErrorMessage = "Не выбрано подразделение сотрудника")]
        public Guid DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Не выбрана должность сотрудника")]
        public Guid FunctionId { get; set; }

        [Required(ErrorMessage = "Не выбран график работы")]
        public Guid ScheduleId { get; set; }

        public bool IsConcurrent { get; set; }

        [Range(0.125F, 1F, ErrorMessage = "Значение доли ставки должно лежать в диапазоне от 0.125 до 1.0")]
        [Required(ErrorMessage = "Не задана доля ставки")]
        public float Rate { get; set; } = 1;

        public string? Reason { get; set; }



        public SelectList? AvailableFunctions { get; set; }

        public SelectList? AvailableDepartments { get; set; }

        public SelectList? AvailableSchedules { get; set; }
    }
}
