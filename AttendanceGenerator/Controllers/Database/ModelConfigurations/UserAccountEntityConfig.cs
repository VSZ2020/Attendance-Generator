using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using AttendanceGenerator.Model.Session.Permissions;
using AttendanceGenerator.Model.Session.Role;
using AttendanceGenerator.Model.Session.UserAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.ModelConfigurations
{
    public class UserAccountEntityConfig : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            //Привязываем аккаунт к Id учреждения
            builder.HasOne(ua => ua.Establishment).WithMany().HasForeignKey(p => p.EstablishmentId);
            //Привязываем аккаунт к Id роли
            builder.HasOne(role => role.UserRole).WithMany().HasForeignKey(r => r.RoleId).IsRequired();
            builder.HasData(new UserAccount()
            {
                Id = 1,
                Username = "Administrator",
                Login = "admin",
                Password = "admin",
                Email = "v.iz.19@yandex.ru",
                RoleId = 1
            });
        }
    }
}
