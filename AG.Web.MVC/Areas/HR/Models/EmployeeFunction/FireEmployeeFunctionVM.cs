using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Areas.HR.Models.EmployeeFunction
{
    public class FireEmployeeFunctionVM
    {
        [Required(ErrorMessage = "Отсутствует идентификатор записи")]
        public Guid RecordId { get; set; }

        public Guid? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string? DepartmentName { get; set; }

        public string? FunctionName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Укажите дату окончания работы на должности")]
        public DateTime? FiredDate { get; set; }
    }
}
