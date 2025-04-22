using AG.Core.Enums;

namespace AG.Web.MVC.Models.CorrectionDay
{
    public class CorrectionDayVM
    {
        public Guid Id { get; set; }
        public int? Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public string Date { get; set; }

        public string DayType { get; set; }

        public string Title { get; set; }

        public float Hours { get; set; }
    }
}
