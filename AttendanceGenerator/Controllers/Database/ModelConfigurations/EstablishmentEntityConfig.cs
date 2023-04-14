using AttendanceGenerator.Model.Department;
using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using AttendanceGenerator.Model.Establishment;
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
    public class EstablishmentEntityConfig : IEntityTypeConfiguration<Establishment>
    {
        public void Configure(EntityTypeBuilder<Establishment> builder)
        {
            //Таблица Establishments теперь включает в себя поля класса WeekTimeConfiguration
            builder.OwnsOne(p => p.WeekConfiguration);

            //У учреждения может быть несколько отделов, но у отдела может быть только одно учреждение
            builder.HasMany(est => est.Departments).WithOne().HasForeignKey(dep=>dep.EstablishmentId).IsRequired();
            
        }
    }
}
