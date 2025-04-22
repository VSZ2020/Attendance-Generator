using AG.Core.Enums;

namespace AG.Core.Extensions
{
    public static class TimesheetForm
    {
        public static string GetTimesheetFormName(this TimesheetFormType? type)
        {
            return type switch
            {
                TimesheetFormType.FormT12 => "№ Т-12",
                TimesheetFormType.FormT13 => "№ Т-13",
                TimesheetFormType.Form0504421 => "№ 0504421",
                _ => ""
            };
        }
    }
}
