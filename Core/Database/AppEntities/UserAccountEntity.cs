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
        public string? Email { get; set; }

        public string? Roles { get; set; }

        public DateTime SessionExpiredAt { get; set; }
        public string? PasswordHash { get; set; }

        public int? DepartmentId { get; set; }

        //TODO: Реализовать сохранение в БД
        public string DepartmentIds { get; set; }
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
                    Roles = RolesDefault.ADMINISTRATOR,
                    DepartmentIds = ""
                },
                new UserAccountEntity() {
                    Id = id++,
                    UserName = "Модератор",
                    Login = "moder",
                    Email = "",
                    PasswordHash = "",
                    Roles = RolesDefault.MODERATOR,
					DepartmentIds = ""
				},
                new UserAccountEntity() {
                    Id = id++,
                    UserName = "Обычный Пользователь",
                    Login = "user",
                    Email = "",
                    PasswordHash = "",
                    Roles = RolesDefault.USER,
                    DepartmentId = 1,
					DepartmentIds = "1"
				}
            };
        }
    }
}
