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
    [Table(DBUtils.TABLE_FUNCTION_GROUPS)]
    public class FunctionGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Function>? Functions { get; set; }

        public FunctionGroup(int id, string groupName)
        {
            Id = id;
            Name = groupName;
        }
        public FunctionGroup() { }
    }
}
