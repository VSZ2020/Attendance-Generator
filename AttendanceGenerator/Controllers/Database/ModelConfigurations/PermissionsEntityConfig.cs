using AttendanceGenerator.Model.Employees.EmployeeFunctions;
using AttendanceGenerator.Model.Session.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Controllers.Database.ModelConfigurations
{
    public class PermissionsEntityConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            int permID = 1;
            var perm_permissionsView = new Permission() { Id = permID++, Name = "Просмотр разрешений", Action = "permissions_view", Description = "" };
            var perm_permissionsEdit = new Permission() { Id = permID++, Name = "Редактирование разрешений", Action = "permissions_edit", Description = "" };
            var perm_employeesView = new Permission() { Id = permID++, Name = "Просмотр сотрудников", Action = "employees_view", Description = "" };
            var perm_employeeAdd = new Permission() { Id = permID++, Name = "Добавление сотрудника", Action = "employee_add", Description = "" };
            var perm_employeeEdit = new Permission() { Id = permID++, Name = "Редактирование сотрудника", Action = "employee_edit", Description = "" };
            var perm_employeeRemove = new Permission() { Id = permID++, Name = "Удаление сотрудника", Action = "employee_remove", Description = "" };
            var perm_EstablishmentsView = new Permission() { Id = permID++, Name = "Просмотр учреждений", Action = "establishments_view", Description = "" };
            var perm_EstablishmentAdd = new Permission() { Id = permID++, Name = "Добавление учреждения", Action = "establishment_add", Description = "" };
            var perm_EstablishmentEdit = new Permission() { Id = permID++, Name = "Редактирование учреждения", Action = "establishment_edit", Description = "" };
            var perm_EstablishmentRemove = new Permission() { Id = permID++, Name = "Удаление учреждения", Action = "establishment_remove", Description = "" };
            var perm_departmentsView = new Permission() { Id = permID++, Name = "Просмотр отделений", Action = "departments_view", Description = "" };
            var perm_departmentAdd = new Permission() { Id = permID++, Name = "Добавление отдела", Action = "department_add", Description = "" };
            var perm_departmentEdit = new Permission() { Id = permID++, Name = "Редактирование отдела", Action = "department_edit", Description = "" };
            var perm_departmentRemove = new Permission() { Id = permID++, Name = "Удаление отдела", Action = "department_remove", Description = "" };
            var perm_funcionsView = new Permission() { Id = permID++, Name = "Просмотр должностей", Action = "functions_view", Description = "" };
            var perm_funcionAdd = new Permission() { Id = permID++, Name = "Добавление должности", Action = "function_add", Description = "" };
            var perm_functionEdit = new Permission() { Id = permID++, Name = "Редактирование должности", Action = "function_edit", Description = "" };
            var perm_functionRemove = new Permission() { Id = permID++, Name = "Удаление должности", Action = "function_remove", Description = "" };

            builder.HasData(perm_permissionsView);
            builder.HasData(perm_permissionsEdit);
            builder.HasData(perm_employeesView);
            builder.HasData(perm_employeeAdd);
            builder.HasData(perm_employeeEdit);
            builder.HasData(perm_employeeRemove);
            builder.HasData(perm_EstablishmentsView);
            builder.HasData(perm_EstablishmentAdd);
            builder.HasData(perm_EstablishmentEdit);
            builder.HasData(perm_EstablishmentRemove);
            builder.HasData(perm_departmentsView);
            builder.HasData(perm_departmentAdd);
            builder.HasData(perm_departmentEdit);
            builder.HasData(perm_departmentRemove);
            builder.HasData(perm_funcionsView);
            builder.HasData(perm_funcionAdd);
            builder.HasData(perm_functionEdit);
            builder.HasData(perm_functionRemove);
        }
    }
}
