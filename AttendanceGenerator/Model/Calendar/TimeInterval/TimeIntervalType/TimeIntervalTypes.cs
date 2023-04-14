using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType
{
    /// <summary>
    /// Хранит перечень видов временных интервалов
    /// </summary>
    public class TimeIntervalTypes
    {
        public enum ETimeIntervalType
        {
            Undefined = -1,
            UserDefined = 0,
            Vacation = 1,
            Business = 2,
            SickLeave = 3,
            EducationVacation = 4,
            UnpaidVacation = 5,
            Other = 6
        }
        /// <summary>
        /// Список доступных типов интервалов времени
        /// </summary>
        public static Dictionary<ETimeIntervalType,ITimeIntervalType> Types = new Dictionary<ETimeIntervalType,ITimeIntervalType>()
        {

            {ETimeIntervalType.Vacation, new TimeIntervalType_Vacation() },
            {ETimeIntervalType.Business, new TimeIntervalType_Business()},
            {ETimeIntervalType.SickLeave, new TimeIntervalType_SickLeave()},
            {ETimeIntervalType.EducationVacation, new TimeIntervalType_EducationVacation()},
            {ETimeIntervalType.UnpaidVacation, new TimeIntervalType_UnpaidVacation()},
            {ETimeIntervalType.Other, new TimeIntervalType_Other()}

        };
        /// <summary>
        /// Возвращает тип временного интервала по-умолчанию (Отпуск)
        /// </summary>
        /// <returns></returns>
        public static ITimeIntervalType GetDefaultTimeInterval() => Types[ETimeIntervalType.Vacation];

        /// <summary>
        /// Возвращает интервал заданного типа по ID
        /// </summary>
        /// <param name="id">Идентификатор типа интервала</param>
        /// <returns></returns>
        public static ITimeIntervalType GetByType(ETimeIntervalType intervalType)
        {
            if (Types.ContainsKey(intervalType))
                return Types[intervalType];
            return new TimeIntervalType_UserDefined();
        }

        public static ITimeIntervalType GetByID(int id)
        {
            foreach (var interval in Types.Keys)
                if ((int)interval == id)
                    return Types[interval];
            return new TimeIntervalType_UserDefined();
        }
    }
}
