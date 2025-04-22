namespace AG.Web.MVC.Models.Timesheet
{
    public class Form0504421ContentVM : BaseTimesheetContentVM
    {
        public List<Form0504421RowVM> Rows { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string EstablishmentName { get; set; } = string.Empty;

        public DateTime LastModifiedAt { get; set; }
    }
}
