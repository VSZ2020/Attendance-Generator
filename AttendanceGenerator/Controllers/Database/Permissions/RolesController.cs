using AttendanceGenerator.Model.Session.Permissions;
using AttendanceGenerator.Model.Session.Role;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.Permissions
{
    public class RolesController
    {
        /// <summary>
        /// Добавялет новую роль в базу
        /// </summary>
        /// <param name="role"></param>
        public static void AddRole(Role role)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Удаляет роль из базы
        /// </summary>
        /// <param name="role"></param>
        public static void RemoveRole(Role role)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Roles.Remove(role);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Обновляет данные роли в базе
        /// </summary>
        /// <param name="role"></param>
        public static void UpdateRole(Role role)
        {
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                context.Roles.Update(role);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Выводит список доступных ролей
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetRoles()
        {
            List<Role> roles;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                roles = context.Roles.ToList();
            }
            return (roles != null) ? roles : new List<Role>();
        }
        /// <summary>
        /// Возвращает список разрешений для данной роли
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static List<Permission> GetRolePermissions(int roleID)
        {
            List<Permission>? permissions = null;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                permissions = context.Roles.Where(role => role.Id == roleID).FirstOrDefault().Permissions;
            }
            return permissions != null ? permissions : new List<Permission>();
        }
    }
}
