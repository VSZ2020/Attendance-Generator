using AttendanceGenerator.Controllers.Database;
using AttendanceGenerator.Model.Session.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Session.Role
{
    public class Role
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Список разрешений данной роли
        /// </summary>
        public List<Permission>? Permissions { get; set; }

        public Role() { }

        /// <summary>
        /// Проверяет, что роли выдано разрешение на указанное действие
        /// </summary>
        /// <param name="permissionAction"></param>
        /// <returns></returns>
        public bool IsActionGranted(string permissionAction)
        {
            return this.Permissions.Where(perm => perm.Action.Equals(permissionAction)).Count() > 0;
        }
    }
}
