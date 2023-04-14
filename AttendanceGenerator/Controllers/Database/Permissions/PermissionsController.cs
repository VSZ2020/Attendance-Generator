using AttendanceGenerator.Model.Session.Permissions;
using AttendanceGenerator.Model.Session.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.Permissions
{
    public class PermissionsController
    {
        public static List<Permission> GetPermissions(int roleID)
        {
            List<Permission>? permissions = null;
            using (ApplicationDbContext context = ApplicationDbContext.GetContext())
            {
                var dbPermissions = context.Permissions;
                if (roleID == -1)
                    permissions = dbPermissions.ToList();
                else
                {
                    permissions = context.Roles.Where(role => role.Id == roleID).FirstOrDefault().Permissions;
                }
            }
            return permissions != null ? permissions : new List<Permission>();
        }

        /// <summary>
        /// Проверяет наличие разрешения у данной роли пользователя
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permissionAction"></param>
        /// <returns></returns>
        public static bool IsActionGranted(Role role, string permissionAction)
        {
            return role.Permissions.Where(perm => perm.Action.Equals(permissionAction)).Count() > 0;
        }

        /// <summary>
        /// Проверяет наличие разрешения у данной роли пользователя
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool IsActionGranted(Role role, Permission permission)
        {
            return IsActionGranted(role, permission.Action);
        }
    }
}
