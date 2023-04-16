using System.Collections.Generic;
using System.Linq;

namespace Core.Security
{
    public class UserRole
    {
        public UserRoleType  RoleType { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список доступных разрешений для роли
        /// </summary>
        public IList<PermissionType> Permissions { get; set; }

        /// <summary>
        /// Проверка наличия разрешения у роли
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool HasAccess(PermissionType permission) => Permissions?.Contains(permission) ?? false;

        public static Dictionary<UserRoleType, UserRole> AvailableRoles = new Dictionary<UserRoleType, UserRole>()
        {
            {
                UserRoleType.Administrator,
                new UserRole(){
                    RoleType = UserRoleType.Administrator,
                    Name = "Администратор",
                    Permissions = Permission.GetDefaults().Select(p => p.Key).ToList()
            }},
            {
                UserRoleType.Moderator,
                new UserRole()
                {
                    RoleType = UserRoleType.Moderator,
                    Name = "Модератор",
                    Permissions = new List<PermissionType>()
                    {
                        PermissionType.ViewAllEmployees,
                        PermissionType.ViewAllDepartments,
                        PermissionType.ViewAllUserAccounts,
                        PermissionType.AddDepartment,
                        PermissionType.AddEmployee,
                        PermissionType.AddFunction,
                        PermissionType.AddTimeInterval,
                        PermissionType.AddUserAccount,
                        PermissionType.EditDepartment,
                        PermissionType.EditEmployee,
                        PermissionType.EditUserAccount,
                        PermissionType.EditFunction,
                        PermissionType.EditTimeInterval

                    }
                }
            },
            {
                UserRoleType.User,
                new UserRole()
                {
                    RoleType = UserRoleType.User,
                    Name = "Пользователь",
                    Permissions = new List<PermissionType>()
                    {

                    }
                }
            }

        };
    }
}
