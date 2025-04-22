using AG.Core.Enums;

namespace AG.Core.Models
{
    public class CorrectionDay
    {
        public int? Year { get; set; }
        
        public int Month { get; set; }
        
        public int Day { get; set; }

        public string Title { get; set; }

        public CorrectionDayType Type { get; set; }

        public float Hours { get; set; } = 0;
    }
}