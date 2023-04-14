using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Отпуск учебный
    /// </summary>
    public class TimeIntervalType_EducationVacation : ITimeIntervalType
    {
        public TimeIntervalTypes.ETimeIntervalType IntervalType => TimeIntervalTypes.ETimeIntervalType.EducationVacation;
        public string Title => App.Current.Resources["IntervalType_Title_EducationVacation"].ToString() ?? "[Отпуск учебный]";

        public string ShortTitle => App.Current.Resources["IntervalType_ShortTitle_EducationVacation"].ToString() ?? "[ОУ]";

        public string Decription { get; set; } = string.Empty;
    }
}
