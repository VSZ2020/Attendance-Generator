using AG.Core.Enums;

namespace AG.Core.Models
{
    public class TimeInterval
    {
        public int CODE { get; set; }
        
        public string Title { get; set; }
        
        public string ShortTitle { get; set; }

        public string? Reason { get; set; }
        
        public DayType DayType { get; set; }

    }
}