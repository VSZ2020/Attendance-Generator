using Core.Database.Entities;
using Core.Security;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
    public class EmployeesService
    {
        public EmployeesService(IEstablishmentItemsRepository repo) { this.establishmentsRepository = repo; }

        private readonly IEstablishmentItemsRepository establishmentsRepository;

        public virtual DatabaseResponse<Employee> GetAvailableEmployees(UserAccount user, int departmentId = 0)
        {
            if (user == null || !UserRole.HasAccess(user.RoleType, PermissionType.ViewEmployees))
                return new DatabaseResponse<Employee>(
                    DatabaseResponse<Employee>.ResponseCode.PermissionsError,
                    new List<Employee>(),
                    "Нет прав на просмотр списка сотрудников");

            IList<EmployeeEntity> employees;
            //Если у пользователя есть права на просмотр всех сотрудников, то возвращаем список всех сотрудников
            if (departmentId == 0 && UserRole.HasAccess(user.RoleType, PermissionType.ViewAllEmployees))
                employees = establishmentsRepository.GetAll<EmployeeEntity>();
            else
                employees = establishmentsRepository.GetAll<EmployeeEntity>(empl => empl.DepartmentId == departmentId);

            return employees == null ?
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.NoData, null, "В базе нет сотрудников, отвечающих заданным параметрам") :
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.Success, employees.Select(DbEntityToEmployee).ToList());
        }

        protected virtual Employee DbEntityToEmployee(EmployeeEntity employee)
        {
            var department = establishmentsRepository.GetById<DepartmentEntity>(employee.DepartmentId).Name;
            var function = establishmentsRepository.GetById<FunctionEntity>(employee.FunctionId).Name;
            var status = establishmentsRepository.GetById<EmployeeStatusEntity>(employee.StatusId).Name;

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
    }
}
