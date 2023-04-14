using AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration;
using AttendanceGenerator.Model.Department;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Establishment
{
    /// <summary>
    /// Описывает Учреждение (Организацию)
    /// </summary>
    public class Establishment
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименование учреждения
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        public WeekTimeConfiguration? WeekConfiguration { get; set; }

        /// <summary>
        /// Перечень отделов в организации
        /// </summary>
        public List<Department.Department>? Departments { get; set; }

        public Establishment()
        {
            WeekConfiguration = new WeekTimeConfiguration();
            Departments = new List<Department.Department>();
        }
    }
}
