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
    public class FunctionEntityConfig : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            int funcID = 1;
            builder.HasData(new Function(1, "Инженер", "инженер") { Id = funcID++ });
            builder.HasData(new Function(1, "Инженер-исследователь", "инж.-иссл.") { Id = funcID++ });
            builder.HasData(new Function(1, "Лаборант", "лабор.") { Id = funcID++ });
            builder.HasData(new Function(1, "Младший научный сотрудник", "м.н.с.") { Id = funcID++ });
            builder.HasData(new Function(1, "Научный сотрудник", "н.с.") { Id = funcID++ });
            builder.HasData(new Function(1, "Старший научный сотрудник", "с.н.с.") { Id = funcID++ });
            builder.HasData(new Function(1, "Ведущий научный сотрудник", "в.н.с.") { Id = funcID++ });
            builder.HasData(new Function(1, "Главный научный сотрудник", "г.н.с.") { Id = funcID++ });

            builder.HasData(new Function(2, "Директор", "дир.") { Id = funcID++ });
            builder.HasData(new Function(2, "Заместитель директора", "зам.дир.") { Id = funcID++ });
            builder.HasData(new Function(2, "Помощник директора", "пом.дир.") { Id = funcID++ });
            builder.HasData(new Function(2, "Заведующий лабораторией", "зав.лаб.") { Id = funcID++ });
            builder.HasData(new Function(2, "Заместитель заведующего лабораторией", "зам.зав.лаб.") { Id = funcID++ });
            builder.HasData(new Function(2, "Ученый секретарь", "уч.секр.") { Id = funcID++ });
            builder.HasData(new Function(2, "Кадровый специалист", "кадр.спец.") { Id = funcID++ });
            builder.HasData(new Function(2, "Ведущий кадровый специалист", "вед.кадр.спец.") { Id = funcID++ });

            builder.HasData(new Function(3, "Главный бухгалтер", "г.бухг.") { Id = funcID++ });
            builder.HasData(new Function(3, "Ведущий бухгалтер", "в.бухг.") { Id = funcID++ });
            builder.HasData(new Function(3, "Бухгалтер", "бухг.") { Id = funcID++ });
            builder.HasData(new Function(3, "Экономист", "экон.") { Id = funcID++ });
        }
    }
}
