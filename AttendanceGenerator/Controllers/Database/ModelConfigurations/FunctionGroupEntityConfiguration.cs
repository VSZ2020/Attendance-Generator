using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.ModelConfigurations
{
    public class FunctionGroupEntityConfiguration : IEntityTypeConfiguration<FunctionGroup>
    {
        public void Configure(EntityTypeBuilder<FunctionGroup> builder)
        {
            builder.HasMany(gr => gr.Functions).WithOne().HasForeignKey(func=>func.GroupId);

            builder.HasData(new FunctionGroup(1, "Научные должности"));
            builder.HasData(new FunctionGroup(2, "Административные должности"));
            builder.HasData(new FunctionGroup(3, "Финансовые должности"));
        }
    }
}
