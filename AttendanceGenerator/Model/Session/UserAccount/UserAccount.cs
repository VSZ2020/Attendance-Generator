using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Establishment;
using AttendanceGenerator.Model.Session.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Session.UserAccount
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя в системе
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Логин для входа в систему
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль для входа в систему
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }

        public int RoleId { get; set; }

        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        //[ForeignKey("RoleId")]
        public Role.Role? UserRole { get; set; }

        public int? EstablishmentId { get; set; }
        /// <summary>
        /// Учреждение пользователя
        /// </summary>
        //[ForeignKey("EstablishmentId")]
        public Establishment.Establishment? Establishment { get; set; }

        /// <summary>
        /// Отделение(я), за которое(ые) отвечает пользователь
        /// </summary>
        public List<Department.Department>? Departments { get; set; }

        public UserAccount() { }
    }
}
