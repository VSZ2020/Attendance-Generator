using AG.Core.Enums;

namespace AG.Web.MVC.Resources
{
    public class CommonLists
    {
        public static Dictionary<int, string> Months = new Dictionary<int, string>()
        {
            {1, "Январь" },
            {2, "Февраль"},
            {3, "Март"},
            {4, "Апрель"},
            {5, "Май"},
            {6, "Июнь"},
            {7, "Июль"},
            {8, "Август"},
            {9, "Сентябрь"},
            {10, "Октябрь"},
            {11, "Ноябрь"},
            {12, "Декабрь"},
        };

        public static Dictionary<CorrectionDayType, string> NamesOfCorrectionDays = new()
        {
            { CorrectionDayType.Working, "Рабочий" },
            { CorrectionDayType.PreHoliday, "Предпраздничный" },
            { CorrectionDayType.DayOff, "Выходной" },
        };


        public static string CaseChange(string monthName)
        {
            string c = "";
            switch (monthName[monthName.Length - 1])
            {
                case 'т':
                    c = "та";
                    break;
                default:
                    c = "я";
                    break;
            }
            return monthName.Substring(0, monthName.Length - 1) + c;
        }

        public static string GetCaseChangedMonth(int monthId) => CaseChange(Months[monthId]);
    }
}
