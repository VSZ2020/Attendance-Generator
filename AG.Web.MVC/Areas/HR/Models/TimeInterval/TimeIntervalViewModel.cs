namespace AG.Web.MVC.Areas.HR.Models.TimeInterval
{
    public record class TimeIntervalViewModel
    {
        public Guid Id { get; set; }

        public int CODE { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
    }
}
