using AttendanceGenerator.Controllers.Database;
using AttendanceGenerator.Model.Calendar.TimeInterval.TimeIntervalType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Calendar.TimeInterval
{
    public class TimeInterval
    {
        /// <summary>
        /// Формат времени, используемый для представления
        /// </summary>
        [NotMapped]
        public const string DateTimeFormat = "dd.MM.yyyy";

        /// <summary>
        /// Идентификатор интервала
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Идентификатор пользователя, которому принадлежит интервал
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Пользовательское название интервала
        /// </summary>
        public string IntervalName { get; set; }

        /// <summary>
        /// Тип временного интервала:
        /// Отпуск, Больничный, Командировка и т.д.
        /// </summary>
        public TimeIntervalTypes.ETimeIntervalType IntervalType { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Конструктор временнОго интервала
        /// </summary>
        /// <param name="id">Идентификатор интервала</param>
        /// <param name="type">Тип периода</param>
        /// <param name="from">Начало интервала</param>
        /// <param name="to">Окончание интервала (включительно)</param>
        /// <param name="custom_name">Пользовательское название</param>
        public TimeInterval(int id, TimeIntervalTypes.ETimeIntervalType type, DateTime from, DateTime to, string custom_name="")
        {
            ID = id;
            IntervalName = custom_name;
            IntervalType = type;
            From = from;
            To = to;
        }

        /// <summary>
        /// Создает временнОй интервал по-умолчанию
        /// </summary>
        /// <param name="id">Идентификатор интервала</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="custom_name">Пользовательское название</param>
        public TimeInterval(int id, DateTime from, DateTime to, string custom_name) : 
            this(id, TimeIntervalTypes.ETimeIntervalType.Other, from, to, custom_name) 
        { }

        public TimeInterval() { }
    }
}
