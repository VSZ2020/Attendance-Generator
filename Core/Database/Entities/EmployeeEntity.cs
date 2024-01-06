using System;
using System.Collections;
using System.Collections.Generic;

namespace Core.Database.Entities
{
	public class EmployeeEntity : BaseEntity
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName {get;set;}

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Доля ставки, занимаемая сотрудником
        /// </summary>
        public float Rate { get; set; }
        
        /// <summary>
        /// Флаг, является ли сотрудник внутренним совместителем
        /// </summary>
        public bool IsInternalConcurrent { get; set; }

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public FunctionEntity? Function { get; set; }

        public Guid FunctionId { get; set; }

        /// <summary>
        /// Отдел работы сотрудника
        /// </summary>
        public DepartmentEntity? Department { get; set; }
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Текущий статус сотрудника
        /// </summary>
        public EmployeeStatusEntity? Status { get; set; }
        public Guid StatusId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public List<TimeIntervalEntity> TimeIntervals { get; set; }
    }
}
