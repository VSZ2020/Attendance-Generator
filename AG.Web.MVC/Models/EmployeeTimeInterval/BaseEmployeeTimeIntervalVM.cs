using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.EmployeeTimeInterval
{
    public class BaseEmployeeTimeIntervalVM
    {
        [Required(ErrorMessage = "Не указана начальная дата")]
        [DataType(DataType.Date)]
        public DateTime Begin { get; set; }

        [Required(ErrorMessage = "Не указана конечная дата")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}
