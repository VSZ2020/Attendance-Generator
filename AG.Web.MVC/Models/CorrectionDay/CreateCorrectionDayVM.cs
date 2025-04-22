using AG.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AG.Web.MVC.Models.CorrectionDay
{
    public class CreateCorrectionDayVM
    {
        [Range(2000,2100, ErrorMessage = "Значение года должно находиться в диапазоне от {1} до {2}")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Не выбран месяц")]
        public int Month { get; set; }

        [Range(1, 31, ErrorMessage = "Номер дня должен находиться в диапазоне от {1} до {2}")]
        [Required(ErrorMessage = "Не указано число")]
        public int Day { get; set; }

        [Required(ErrorMessage = "Не указано название дня")]
        public string Title { get; set; } = string.Empty;

        public CorrectionDayType Type { get; set; }

        public float Hours { get; set; } = 0;

        public List<SelectListItem>? AvailableDayTypes { get; set; }

        public List<SelectListItem>? AvailableMonths { get; set; }
    }
}
