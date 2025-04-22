using System;

namespace AG.Core.Models.Timesheet
{
    [Serializable]
    public class TimeSheetModel
    {
        public TimeSheetModel()
        {
            Content = new Form0504421Content();
        }

        public TimeSheetModel(Form0504421Content content)
        {
            Content = content;
        }

        public Form0504421Content Content { get; set; }
    }
}
