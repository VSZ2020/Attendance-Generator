using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    public interface ITimeIntervalType
    {
        /// <summary>
        /// Тип интервала
        /// </summary>
        TimeIntervalTypes.ETimeIntervalType IntervalType { get; }
        /// <summary>
        /// Название интервала
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Краткое обозначение интервала
        /// </summary>
        string ShortTitle { get; }
        /// <summary>
        /// Описание интервала
        /// </summary>
        string Decription { get; set; }

    }
}
