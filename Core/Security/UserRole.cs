using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Security
{
    [Obsolete("Устаревший класс, представляющий роль ползователя в системе")]
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
                        PermissionType.ViewDepartments,
                        PermissionType.ViewEmployees,
                        PermissionType.ViewEstablishments,
                        PermissionType.ViewUserAccounts,
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
                        PermissionType.ViewDepartments,
                        PermissionType.ViewEmployees,
                        PermissionType.ViewEstablishments,
                        PermissionType.AddEmployee,
                        PermissionType.AddFunction,
                        PermissionType.AddTimeInterval,
                        PermissionType.AddUserAccount,
                        PermissionType.EditEmployee,
                        PermissionType.EditUserAccount,
                        PermissionType.EditFunction,
                        PermissionType.EditTimeInterval
                    }
                }
            }

        };

        public static bool HasAccess(UserRoleType roleType, PermissionType permission)
        {
            return AvailableRoles[roleType].Permissions.Contains(permission);
        }
    }
}
