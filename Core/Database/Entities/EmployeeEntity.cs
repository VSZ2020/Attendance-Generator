using System;
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
        /// Идентификатор должности сотрудника
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// Должности сотрудника
        /// </summary>
        public IList<FunctionEntity> Functions { get; set; }

        /// <summary>
        /// Отдел работы сотрудника
        /// </summary>
        public DepartmentEntity? Department { get; set; }
        public int DepartmentId { get; set; }

        /// <summary>
        /// Текущий статус сотрудника
        /// </summary>
        public EmployeeStatusEntity? Status { get; set; }
        public int StatusId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public static IList<EmployeeEntity> GetDefault()
        {
            return new List<EmployeeEntity>();
        }
    }
}
