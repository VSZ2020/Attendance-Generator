using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using AG.Core.Enums;

namespace AG.Web.MVC.Models.EmployeeTimeInterval
{
    public class CreateEmployeeTimeIntervalVM: BaseEmployeeTimeIntervalVM
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Не выбран тип неявки")]
        public DayType SelectedTimeIntervalType { get; set; }

        //Available time intervals to add
        public SelectList? TimeIntervals { get; set; }
    }
}
