using Core.Database.Entities;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
	public class EmployeesService: IEmployeeService
    {
		#region ctor
		public EmployeesService(IEstablishmentItemsRepository repo) { this.establishmentsRepository = repo; }
		#endregion ctor

		#region fields
		private readonly IEstablishmentItemsRepository establishmentsRepository;
		#endregion fields

		public DatabaseResponse<Employee> GetEmployees(UserAccount user, int departmentId = 0)
        {
            var employees = departmentId > 0 ? 
                establishmentsRepository.GetAll<EmployeeEntity>(e => e.DepartmentId == departmentId) : 
                establishmentsRepository.GetAll<EmployeeEntity>();

            return employees == null ?
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.NoData, null, "В базе нет сотрудников, отвечающих заданным параметрам") :
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.Success, employees.Select(DbEntityToEmployee).ToList());
        }

		#region Utils
		private Employee DbEntityToEmployee(EmployeeEntity employee)
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
		#endregion Utils
	}
}
