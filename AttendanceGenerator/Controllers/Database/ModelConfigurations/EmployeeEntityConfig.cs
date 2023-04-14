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
    public class EmployeeEntityConfig : IEntityTypeConfiguration<Model.Employees.Employee>
    {
        public void Configure(EntityTypeBuilder<Model.Employees.Employee> builder)
        {
            builder.HasOne(empl => empl.Function).WithMany(func => func.Employees);
        }
    }
}
