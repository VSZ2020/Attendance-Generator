using Core.Database.Entities;
using System;
using System.Collections.Generic;

namespace Core.Database.AppEntities
{
    public class PermissionEntity: BaseEntity
    {
        public string Name { get; set; }
        public List<UserRoleEntity> Roles { get; set; }

        public static IList<PermissionEntity> GetDefault()
        {
            throw new NotImplementedException();
        }
    }
}
