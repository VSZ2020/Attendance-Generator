using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using AttendanceGenerator.Model.Session.Permissions;
using AttendanceGenerator.Model.Session.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.ModelConfigurations
{
    public class RolesEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            
            var admin = new Role()
            {
                Id = 1,
                Name = "Администратор",
                Permissions = new List<Permission>()
            };
            var moderator = new Role()
            {
                Id = 2,
                Name = "Модератор",
                Permissions = new List<Permission>()
            };
            var user = new Role()
            {
                Id = 3,
                Name = "Пользователь",
                Permissions = new List<Permission>()
            };
            var guest = new Role()
            {
                Id = 4,
                Name = "Гость",
                Permissions = new List<Permission>()
            };
            builder.HasData(admin);
            builder.HasData(moderator);
            builder.HasData(user);
            builder.HasData(guest);
        }
    }
}
