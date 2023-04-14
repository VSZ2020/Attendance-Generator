using AttendanceGenerator.Controllers.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Employees.EmployeeFunctions
{
    public class Function
    {
        /// <summary>
        /// Идентификатор должности
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор группы, к которой относится должность
        /// </summary>
        [Required]
        public int GroupId { get; set; }
        /// <summary>
        /// Полное название должности работника
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Сокращенное название должности работника
        /// </summary>
        [Required]
        public string ShortName { get; set; }

        public string? DefaultName { get; set; }

        public string? DefaultShortName { get; set; }

        public List<Employee>? Employees { get; set; }

        public Function(int groupID, string fullName, string shortName, string? defaultName = null, string? defaultShortName = null)
        {
            GroupId = groupID;
            Name = fullName;
            ShortName = shortName;
            DefaultName = defaultName ?? fullName;
            DefaultShortName = defaultShortName ?? shortName;
            Employees = new List<Employee>();
        }
        public Function() { }
    }
}
