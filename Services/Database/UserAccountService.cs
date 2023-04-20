using Core;
using Core.Database.AppEntities;
using Core.Database.Entities;
using Core.Security;
using Services.POCO;

namespace Services.Database
{
    public class UserAccountService
    {
        private readonly IRepository<DepartmentEntity> departmentsRepository;
        private readonly IRepository<EmployeeEntity> employeesRepository;
        private readonly IRepository<UserAccountEntity> userAccountRepository;
        private readonly IRepository<FunctionEntity> functionsRepository;
        private readonly IRepository<EmployeeStatusEntity> statusRepository;

        public IList<Department> GetAvailableDepartments(UserAccount user)
        {
            IList<DepartmentEntity> departments;
            if (UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllDepartments))
                departments = departmentsRepository.GetAll();
            else
                departments = departmentsRepository.GetAll(dep => dep.Id == user.DepartmentId);
            return departments == null ?
                new List<Department>() :
                departments.Select(DbEntityToDepartment).ToList();
        }

        public IList<Employee> GetAvailableEmployees(UserAccount user, int departmentId = 0)
        {
            IList<EmployeeEntity> employees;
            if (departmentId == 0 && UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllEmployees))
                employees = employeesRepository.GetAll();
            else
                employees = employeesRepository.GetAll(empl => empl.DepartmentId == departmentId);
                
            return employees
                .Select(DbEntityToEmployee)
                .ToList();
        }

        public IList<UserAccount> GetUserAccounts()
        {
            return userAccountRepository.GetAll().Select(ua => new UserAccount()
            {
                Id = ua.Id,
                Username = ua.UserName,
                Login = ua.Login,
                SessionExpiredAt = ua.SessionExpiredAt,
                DepartmentId = ua.DepartmentId.HasValue ? ua.DepartmentId.Value : 0,
                RoleType = ua.Role
            }).ToList();
        }

        public UserAccountService(
            IRepository<DepartmentEntity> departmentRep, 
            IRepository<EmployeeEntity> employeeRep, 
            IRepository<UserAccountEntity> userAccountRep,
            IRepository<FunctionEntity> functionsRep,
            IRepository<EmployeeStatusEntity> statusRepository)
        {
            this.departmentsRepository = departmentRep;
            this.employeesRepository = employeeRep;
            this.userAccountRepository = userAccountRep;
            this.functionsRepository = functionsRep;
            this.statusRepository = statusRepository;
        }

        #region Utils
        private Employee DbEntityToEmployee(EmployeeEntity employee)
        {
            var department = DbEntityToDepartment(departmentsRepository.GetById(employee.DepartmentId));
            EmployeeFunction function = DbEntityToFunction(functionsRepository.GetById(employee.FunctionId));
            EmployeeStatus status = DbEntityToStatus(statusRepository.GetById(employee.StatusId));
            return new Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Rate = employee.Rate,
                Department = department,
                Function = function,
                IsConcurrent = employee.IsInternalConcurrent,
                PhoneNumber = employee?.PhoneNumber ?? "",
                Status = status
            };
        }

        private Department DbEntityToDepartment(DepartmentEntity? entity)
        {
            return entity == null ?
                new Department() { Id = 0, Name = "Не определено"} :
                new Department()
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
        }

        private EmployeeFunction DbEntityToFunction(FunctionEntity? entity)
        {
            return entity == null ?
                new EmployeeFunction() { Id = 0, Name = "Не определена", ShortName = "Неопр."} :
                new EmployeeFunction() { Id = entity.Id, Name = entity.Name, ShortName = entity.ShortName };
        }

        private EmployeeStatus DbEntityToStatus(EmployeeStatusEntity? entity)
        {
            return entity == null ?
                new EmployeeStatus() { Id = 0, Name = "Не определен" } :
                new EmployeeStatus() { Id = entity.Id, Name = entity.Name };
        }
        #endregion
    }
}
