using Core.Database.Entities;
using Core.Security;
using System;
using System.Collections.Generic;

namespace Core.Database.AppEntities
{
    public class UserAccountEntity: BaseEntity
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Роль пользователя в системе. От роли зависят предоставленные права.
        /// Свойство является устаревшим. Используйте вместо этого свойство <see cref="string">Roles</see>
        /// </summary>
        [Obsolete("Use instead Roles property")]
        public UserRoleType Role { get; set; }

        public string? Roles { get; set; }

        public DateTime SessionExpiredAt { get; set; }
        public string? PasswordHash { get; set; }

        public int? DepartmentId { get; set; }

        //TODO: Реализовать сохранение в БД
        public List<int>? DepartmentsIds { get; set; }
        //public DepartmentEntity? Department { get; set; }

        public static IList<UserAccountEntity> GetDefault()
        {
            int id = 1;
            return new List<UserAccountEntity>
            {
                new UserAccountEntity() {
                    Id = id++,
                    UserName = "Администратор",
                    Login = "admin",
                    Email = "admin@admin.org",
                    PasswordHash = "",
                    Role = UserRoleType.Administrator,
                    Roles = RolesDefault.ADMINISTRATOR
                },
                new UserAccountEntity() {
                    Id = id++,
                    UserName = "Модератор",
                    Login = "moder",
                    Email = "",
                    PasswordHash = "",
                    Role = UserRoleType.Moderator,
                    Roles = RolesDefault.MODERATOR
                },
                new UserAccountEntity() {
                    Id = id++,
                    UserName = "Обычный Пользователь",
                    Login = "user",
                    Email = "",
                    PasswordHash = "",
                    Role = UserRoleType.User,
                    Roles = RolesDefault.USER,
                    DepartmentId = 1
                }
            };
        }
    }
}
