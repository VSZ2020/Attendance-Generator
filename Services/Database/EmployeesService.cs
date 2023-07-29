using Core.Database.Entities;
using Services.Domains;
using SQLiteRepository;

namespace Services.Database
{
	public class EmployeesService: IEmployeeService
    {
		#region ctor
		public EmployeesService(IEstablishmentItemsRepository repo, UserAccount user) { this.establishmentsRepository = repo; this.user = user; }
		#endregion ctor

		#region fields
		private readonly IEstablishmentItemsRepository establishmentsRepository;
        private readonly UserAccount user;
		#endregion fields

		public DatabaseResponse<Employee> GetEmployees(int departmentId = 0)
        {
            var employees = departmentId > 0 ? 
                establishmentsRepository.GetAll<EmployeeEntity>(e => e.DepartmentId == departmentId) : 
                establishmentsRepository.GetAll<EmployeeEntity>();

            return employees == null ?
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.NoData, null, "В базе нет сотрудников, отвечающих заданным параметрам") :
                new DatabaseResponse<Employee>(DatabaseResponse<Employee>.ResponseCode.Success, employees.Select(DbEntityToEmployee).ToList());
        }

        public DatabaseResponse<TimeInterval> GetEmployeePeriods(Employee employee)
        {
            var periods = establishmentsRepository.GetAll<TimeIntervalEntity>(ti => ti.EmployeeId == employee.Id).ToList();
            return new DatabaseResponse<TimeInterval>(DatabaseResponse<TimeInterval>.ResponseCode.Success, periods.Select(DbEntityToTimeInterval).ToList());
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

        private TimeInterval DbEntityToTimeInterval(TimeIntervalEntity entity)
        {
            var intervalType = entity.IntervalType == null ? establishmentsRepository.GetById<TimeIntervalTypeEntity>(entity.IntervalTypeId) : entity.IntervalType;
            return new TimeInterval()
            {
                TimeIntervalType = ToTimeInterval(intervalType),
                Begin = entity.StartDate,
                End = entity.EndDate,
                Comment = entity.Comment ?? ""
            };
        }

        private TimeIntervalType ToTimeInterval(TimeIntervalTypeEntity entity)
        {
            return new TimeIntervalType()
            {
                LongName = entity.Name,
                ShortName = entity.ShortName,
                Description = entity.Description,
            };
        }

		public Task<DatabaseResponse<Employee>> GetEmployeesAsync(int departmentId = 0)
		{
			return Task.FromResult(GetEmployees(departmentId));
		}

		public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(Employee employee)
		{
            return Task.FromResult(GetEmployeePeriods(employee));
		}
		#endregion Utils
	}
}
