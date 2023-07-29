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
        /// Должность сотрудника
        /// </summary>
        public FunctionEntity? Function { get; set; }

        public int FunctionId { get; set; }

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
            int id = 1;
            return new List<EmployeeEntity>()
            {
                new EmployeeEntity(){
                    Id = id++,
                    FirstName = "Имя 1",
                    LastName = "Фамилия 1",
                    MiddleName = "Отчество 1",
                    Email = "user1@user.ru",
                    Rate = 1f,
                    DepartmentId = 1,
                    StatusId = 1,
                    FunctionId = 1,
                    IsInternalConcurrent = false
                },
                new EmployeeEntity(){
                    Id = id++,
                    FirstName = "Имя 2",
                    LastName = "Фамилия 2",
                    MiddleName = "Отчество 2",
                    Email = "user2@user.ru",
                    Rate = 0.5f,
                    DepartmentId = 2,
                    StatusId = 1,
                    FunctionId = 2,
                    IsInternalConcurrent = false
                },
                new EmployeeEntity(){
                    Id = id++,
                    FirstName = "Name",
                    LastName = "Last name",
                    MiddleName = "Middle name",
                    Email = "user3@user.ru",
                    Rate = 0.2f,
                    DepartmentId = 3,
                    StatusId = 1,
                    FunctionId = 3,
                    IsInternalConcurrent = false
                },
            };
        }
    }
}
