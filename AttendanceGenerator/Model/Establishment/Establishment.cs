using AttendanceGenerator.Model.Calendar.WorkingWeek.TimeConfiguration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
