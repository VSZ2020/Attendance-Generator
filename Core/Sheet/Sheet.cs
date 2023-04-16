using Core.Database.Entities;
using System;

namespace Core.Sheet
{
    public class Sheet
    {
        /// <summary>
        /// Название табеля
        /// </summary>
        public string Name { get; set; } = "Без названия";

        /// <summary>
        /// Номер табеля
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Вид табеля:
        /// 0 - первичный,
        /// 1 - корректирующий, и т.д.
        /// </summary>
        public int SheetType { get; set; }

        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get; set; }

        /// <summary>
        /// Учреждение
        /// </summary>
        public EstablishmentEntity? Establishment { get; set; }

        /// <summary>
        /// Отдел, для которого генерируется табель
        /// </summary>
        public DepartmentEntity? Department { get; set; }

        /// <summary>
        /// Период ведения табеля
        /// </summary>
        public TimeInterval? AccountingPeriod { get; set; }

        /// <summary>
        /// Дата по ОКПО
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Дата формирования табеля
        /// </summary>
        public DateTime? GenerationTime { get; set; }

        /// <summary>
        /// Ответственный за табель
        /// </summary>
        public EmployeeEntity? SheetResponsible { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public EmployeeEntity? SheetExecutor { get; set; }

        /// <summary>
        /// Финансовый исполнитель (проверяющий бухгалтер)
        /// </summary>
        public EmployeeEntity? FinancialExecutor { get; set; }
    }
}
