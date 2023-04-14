using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceGenerator.Model.Calendar.TimeInterval;
using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Employees;

namespace AttendanceGenerator.Model.Sheet
{
    public interface ISheet
    {
        /// <summary>
        /// Название табеля
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Номер табеля
        /// </summary>
        int Number { get; set; }

        /// <summary>
        /// Вид табеля:
        /// 0 - первичный,
        /// 1 - корректирующий, и т.д.
        /// </summary>
        int SheetType { get; set; }

        /// <summary>
        /// Номер корректировки
        /// </summary>
        int CorrectionNumber { get; set; }

        /// <summary>
        /// Учреждение
        /// </summary>
        Establishment.Establishment Establishment { get; set; }

        /// <summary>
        /// Отдел, для которого генерируется табель
        /// </summary>
        Department.Department Department { get; set; }

        /// <summary>
        /// Период ведения табеля
        /// </summary>
        TimeInterval AccountingPeriod { get; set; }

        /// <summary>
        /// Дата по ОКПО
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Дата формирования табеля
        /// </summary>
        DateTime GenerationTime { get; set; }

        /// <summary>
        /// Ответственный за табель
        /// </summary>
        Employee SheetResponsible { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        Employee SheetExecutor { get; set; }

        /// <summary>
        /// Финансовый исполнитель (проверяющий бухгалтер)
        /// </summary>
        Employee FinancialExecutor { get; set; }
    }
}
