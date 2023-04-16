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
        /// Роль пользователя в системе. От роли зависят предоставленные права
        /// </summary>
        public UserRoleType Role { get; set; }
        public DateTime SessionExpiredAt { get; set; }
        public string PasswordHash { get; set; }

        public int? DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }

        public static IList<UserAccountEntity> GetDefault()
        {
            int id = 1;
            return new List<UserAccountEntity>
            {
                new UserAccountEntity() { 
                    Id = id++, 
                    UserName = "Администратор", 
                    Login = "admin", 
                    Email = "",
                    PasswordHash = "",
                    Role = UserRoleType.Administrator
                }
            };
        }
    }
}
