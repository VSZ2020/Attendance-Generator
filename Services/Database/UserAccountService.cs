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

        public IList<Department> GetAvailableDepartments(UserAccount user)
        {
            IList<DepartmentEntity> departments;
            if (UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllDepartments))
                departments = departmentsRepository.GetAll();
            else
                departments = departmentsRepository.GetAll(dep => dep.Id == user.DepartmentId);
            return departments == null ?
                new List<Department>() :
                departments.Select(d => new Department()
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList();
        }

        public IList<Employee> GetAvailableEmployees(UserAccount user, int departmentId = 0)
        {
            IList<EmployeeEntity> employees;
            if (UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllEmployees))
                employees = departmentId == 0 ? employeesRepository.GetAll() : employeesRepository.GetAll(empl => empl.DepartmentId == departmentId);
            else
                employees = employeesRepository.GetAll(empl => empl.DepartmentId == user.DepartmentId);
            return employees == null ?
                new List<Employee>() :
                employees
                .Select(DbEntityToEmployeeConverter)
                .ToList();
        }

        public UserAccountService(
            IRepository<DepartmentEntity> departmentRep, 
            IRepository<EmployeeEntity> employeeRep, 
            IRepository<UserAccountEntity> userAccountRep)
        {
            this.departmentsRepository = departmentRep;
            this.employeesRepository = employeeRep;
            this.userAccountRepository = userAccountRep;
        }

        #region Utils
        private Employee DbEntityToEmployeeConverter(EmployeeEntity employee)
        {
            var department = DbEntityToDepartmentConverter(departmentsRepository.GetById(employee.DepartmentId));
            return new Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Rate = employee.Rate,
                Department = department,

            };
        }

        private Department DbEntityToDepartmentConverter(DepartmentEntity entity)
        {
            return new Department()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
        #endregion
    }
}
