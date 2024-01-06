using Core.Database.Entities;
using System;

namespace Core.Database.AppEntities
{
	public class UserAccountEntity: BaseEntity
    {
        public string UserName { get; set; }

        public string Login { get; set; }

        public string? Email { get; set; }

        public string[] Roles { get; set; }

        public DateTime SessionExpiredAt { get; set; }
        public string? PasswordHash { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid EstablishmentId { get; set; }
    }
}
