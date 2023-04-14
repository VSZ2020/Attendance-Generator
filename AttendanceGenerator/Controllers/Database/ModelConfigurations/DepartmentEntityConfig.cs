using AttendanceGenerator.Model.Department;
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
    public class DepartmentEntityConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Сотрудник может принадлежать только к одному отделу
            builder.HasMany(dep => dep.Employees).WithOne().HasForeignKey(p=>p.DepartmentId).IsRequired();
        }
    }
}
