using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Employees
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string MiddleName { get; set; }

        public Function? Function { get; set; }
        [Required]
        public float Rate { get; set; }

        public bool IsConcurrentWorker { get; set; } = false;
        /// <summary>
        /// Идентификатор отдела, в котором работает сотрудник
        /// </summary>
        public int DepartmentId { get; set; }
        //public Department.Department? Department { get; set; }

        public bool IsWorking { get; set; } = true;


        public Employee() { }
    }
}
