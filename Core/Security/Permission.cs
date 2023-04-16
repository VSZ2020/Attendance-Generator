using System.Collections.Generic;

namespace Core.Security
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Dictionary<PermissionType, Permission> GetDefaults()
        {
            return new Dictionary<PermissionType, Permission>()
            {
                {PermissionType.ViewAllDepartments, new Permission(){ Id = 1, Name = "Просмотр всех подразделений"} },
                {PermissionType.ViewAllEstablishments, new Permission(){ Id = 2, Name = "Просмотр всех организаций"} },
                {PermissionType.ViewAllEmployees, new Permission(){ Id = 3, Name = "Просмотр всех сотрудников"} },
                {PermissionType.ViewAllUserAccounts, new Permission(){ Id = 4, Name = "Просмотр всех аккаунтов"} },
                {PermissionType.AddDepartment, new Permission(){ Id = 5, Name = "Добавить подразделение"} },
                {PermissionType.AddEmployee, new Permission(){ Id = 6, Name = "Добавить сотрудника"} },
                {PermissionType.AddUserAccount, new Permission(){ Id = 7, Name = "Добавить аккаунт пользователя"} },
                {PermissionType.AddTimeInterval, new Permission(){ Id = 8, Name = "Добавить интервал"} },
            };
        }
    }
}
