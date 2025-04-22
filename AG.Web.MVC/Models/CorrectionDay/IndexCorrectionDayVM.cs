using Microsoft.AspNetCore.Mvc.Rendering;

namespace AG.Web.MVC.Models.CorrectionDay
{
    public class IndexCorrectionDayVM
    {
        public int? FilterYear { get; set; }

        public int? FilterMonth { get; set; }

        public IEnumerable<CorrectionDayVM> CorrectionDays { get; set; }

        public IEnumerable<SelectListItem>? AvailableMonths { get; set; }
    }
}
