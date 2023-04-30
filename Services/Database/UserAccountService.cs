using Core.Database.AppEntities;
using Core.Database.Entities;
using Core.Security;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
    public class UserAccountService
    {
        private readonly IItemsRepository<SQLiteRepository.AppContext> usersRepository;
        private readonly IItemsRepository<EstablishmentContext> establishmentsRepository;

        public virtual DatabaseResponse<Department> GetAvailableDepartments(UserAccount user, bool loadEmployees = false)
        {
            if (user == null || !UserRole.HasAccess(user.RoleType, PermissionType.ViewDepartments))
                return new DatabaseResponse<Department>(
                    DatabaseResponse<Department>.ResponseCode.PermissionsError,
                    new List<Department>(),
                    "Нет прав на просмотр списка подразделений");

            IList<DepartmentEntity> departments;
            if (UserRole.AvailableRoles[user.RoleType].HasAccess(PermissionType.ViewAllDepartments))
                departments = establishmentsRepository.GetAll<DepartmentEntity>();
            else
                departments = establishmentsRepository.GetAll<DepartmentEntity>(dep => dep.Id == user.DepartmentId);
            
            if (loadEmployees)
            {
                foreach(var dep in departments)
                {
                    dep.Employees = establishmentsRepository.GetAll<EmployeeEntity>(e => e.DepartmentId == dep.Id);
                }
            }

            return departments == null ?
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.NoData, null, "В базе нет подразделений для указанных параметров") :
                new DatabaseResponse<Department>(DatabaseResponse<Department>.ResponseCode.Success, departments.Select(DbEntityToDepartment).ToList());
        }

        public virtual DatabaseResponse<Employee> GetAvailableEmployees(UserAccount user, int departmentId = 0)
        {
            if (user == null || !UserRole.HasAccess(user.RoleType, PermissionType.ViewEmployees))
                return new DatabaseResponse<Employee>(
                    DatabaseResponse<Employee>.ResponseCode.PermissionsError,
                    new List<Employee>(),
                    "Нет прав на просмотр списка сотрудников");

            IList<EmployeeEntity> employees;
            if (departmentId == 0 && UserRole.HasAccess(user.RoleType, PermissionType.ViewAllEmployees))
                employees = establishmentsRepository.GetAll<EmployeeEntity>();
            else
                employees = establishmentsRepository.GetAll<EmployeeEntity>(empl => empl.DepartmentId == departmentId);

            return employees == null ?
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.NoData, null, "В базе нет сотрудников, отвечающих заданным параметрам"):
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.Success, employees.Select(DbEntityToEmployee).ToList());
        }

        public virtual DatabaseResponse<UserAccount> GetUserAccounts(UserRoleType role)
        {
            if (usersRepository == null || !UserRole.HasAccess(role, PermissionType.ViewUserAccounts))
                return new DatabaseResponse<UserAccount>(
                    DatabaseResponse<UserAccount>.ResponseCode.PermissionsError,
                    new List<UserAccount>(),
                    "Нет прав на просмотр списка пользователей");

            IList<UserAccountEntity> userAccounts;
            if (UserRole.HasAccess(role, PermissionType.ViewAllUserAccounts))
                userAccounts = usersRepository.GetAll<UserAccountEntity>();
            else
                return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.PermissionsError, new List<UserAccount>() { });

            return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.Success, userAccounts.Select(DbEntityToUserAccount).ToList());
        }

        public virtual DatabaseResponse<UserAccount> GetUserById(int id)
        {
            var user = DbEntityToUserAccount(usersRepository.GetById<UserAccountEntity>(id));
            return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.Success,new List<UserAccount>() { user } );
        }

        public virtual void AddDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.AddEntity(ToEntity(dep));
        }

        public virtual void RemoveDepartment(Department? dep)
        {
            if (dep == null)
                throw new ArgumentNullException("Ссылка на подразделение пустая");

            establishmentsRepository.Remove(ToEntity(dep));
        }

        public virtual void RemoveDepartment(int id)
        {
            establishmentsRepository.Remove<DepartmentEntity>(id);
        }

        /// <summary>
        /// Базовый конструктор сервиса
        /// </summary>
        public UserAccountService(IItemsRepository<SQLiteRepository.AppContext> usersRep, IItemsRepository<EstablishmentContext> employeeRep)
        {
            this.usersRepository = usersRep;
            this.establishmentsRepository = employeeRep;
        }

        #region Utils
        protected virtual Employee DbEntityToEmployee(EmployeeEntity employee)
        {
            var department = DbEntityToDepartment(establishmentsRepository.GetById<DepartmentEntity>(employee.DepartmentId));
            EmployeeFunction function = DbEntityToFunction(establishmentsRepository.GetById<FunctionEntity>(employee.FunctionId));
            EmployeeStatus status = DbEntityToStatus(establishmentsRepository.GetById<EmployeeStatusEntity>(employee.StatusId));
            
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

        protected Department DbEntityToDepartment(DepartmentEntity? entity)
        {
            if (entity == null)
                new Department() { Id = 0, Name = "Не определено" };

            IList<Employee> employees = entity.Employees?.Select(DbEntityToEmployee).ToList() ?? new List<Employee>();
            return new Department()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    EstablishmentId = entity.EstablishmentId,
                    Employees = employees
                };
        }

        protected EmployeeFunction DbEntityToFunction(FunctionEntity? entity)
        {
            return entity == null ?
                new EmployeeFunction() { Id = 0, Name = "Не определена", ShortName = "Неопр."} :
                new EmployeeFunction() { Id = entity.Id, Name = entity.Name, ShortName = entity.ShortName };
        }

        protected EmployeeStatus DbEntityToStatus(EmployeeStatusEntity? entity)
        {
            return entity == null ?
                new EmployeeStatus() { Id = 0, Name = "Не определен" } :
                new EmployeeStatus() { Id = entity.Id, Name = entity.Name };
        }

        protected UserAccount DbEntityToUserAccount(UserAccountEntity entity)
        {
            return new UserAccount()
            {
                Id = entity.Id,
                Username = entity.UserName,
                Login = entity.Login,
                SessionExpiredAt = entity.SessionExpiredAt,
                DepartmentId = entity.DepartmentId.HasValue ? entity.DepartmentId.Value : 0,
                RoleType = entity.Role
            };
        }

        protected DepartmentEntity ToEntity(Department department)
        {
            return new DepartmentEntity()
            {
                Id = department.Id,
                Name = department.Name,
                EstablishmentId = department.EstablishmentId
            };
        }
        #endregion
    }
}
