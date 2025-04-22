using AG.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.Timesheet
{
    public class CreateTimesheetVM
    {
        
        [Required (ErrorMessage = "Не выбран месяц")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Не задано начальное число")]
        [Range(0,31, ErrorMessage = "Значение должно быть в диапазоне от 1 до 31")]
        public int BeginDate { get; set; }

        [Required]
        [Range(0,31, ErrorMessage = "Значение должно быть в диапазоне от 1 до 31")]
        public int EndDate { get; set; }

        /// <summary>
        /// Timesheet form type: T-12, T-13 or 0504421
        /// </summary>
        public TimesheetFormType FormType { get; set; }

        /// <summary>
        /// Primary, corrective, etc. 
        /// Default 0 - Primary
        /// </summary>
        public int Kind { get; set; } = 0;

        public string? Comment { get; set; }

        [Required(ErrorMessage = "Не указан исполнитель")]
        public string? ExecutorName { get; set; }
        public string? ExecutorFunction { get; set; }

        [Required(ErrorMessage = "Не указан ответственный исполнитель")]
        public string? ResponsibleExecutorName { get; set; }
        public string? ResponsibleExecutorFunction { get; set; }

        [Required(ErrorMessage = "Не указан проверяющий")]
        public string? AccountingExecutorName { get; set; }
        public string? AccountingExecutorFunction { get; set; }

        public string? DepartmentHeaderName { get; set; }


        public string? DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public SelectList? AvailableExecutors { get; set; }
        public SelectList? AvaialbleKinds { get; set; }
        public SelectList? AvaialbleForms { get; set; }
        
        public SelectList? AvaialbleMonths { get; set; }
    }
}
