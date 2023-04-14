using AttendanceGenerator.Model.Session.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Session.Permissions
{
    /// <summary>
    /// Описывает разрешение
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Идентификатор разрешения
        /// </summary>
        
        public int Id { get; set; }

        /// <summary>
        /// Действие разрешения
        /// </summary>
        
        public string Action { get; set; }

        /// <summary>
        /// Описание разрешения
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// Описание разрешения
        /// </summary>
        public string? Description { get; set; }
        
        public List<Role.Role> Roles { get; set; }
    }
}
