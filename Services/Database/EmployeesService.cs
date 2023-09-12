using Core.Database.Entities;
using Services.Domains;
using Services.Infrastructure.Logger;
using SQLiteRepository;

namespace Services.Database
{
    public class EmployeesService : IEmployeeService
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
            return GetEmployeePeriods(employee.Id);
        }

        public DatabaseResponse<TimeInterval> GetEmployeePeriods(int employeeId)
        {
            var periods = establishmentsRepository.GetAll<TimeIntervalEntity>(ti => ti.EmployeeId == employeeId).ToList();
            return new DatabaseResponse<TimeInterval>(DatabaseResponse<TimeInterval>.ResponseCode.Success, periods.Select(DbEntityToTimeInterval).ToList());
        }

        public void AddEmployeePeriod(int employeeId, TimeInterval timeInterval)
        {
            var entity = ToTimeIntervalEntity(timeInterval, employeeId);
            entity.Id = default;
            establishmentsRepository.AddEntity(entity);

            Logger.Log($"User {user.Username} added employee (ID: {employeeId}) time interval {timeInterval.Begin.ToShortDateString()}-{timeInterval.End.ToShortDateString()}");
        }

        public void UpdateEmployeePeriod(int employeeId, TimeInterval timeInterval)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployeePeriod(int employeeId, TimeInterval timeInterval)
        {
            DeleteEmployeePeriod(timeInterval.Id);
			Logger.Log($"User {user.Username} removed employee (ID: {employeeId}) time interval {timeInterval.Begin.ToShortDateString()}-{timeInterval.End.ToShortDateString()}");
		}

        public void DeleteEmployeePeriod(int timeIntervalId)
        {
            establishmentsRepository.Remove<TimeIntervalEntity>(timeIntervalId);
        }

		public DatabaseResponse<TimeIntervalType> GetTimeIntervalTypes()
        {
            var tit = establishmentsRepository.GetAll<TimeIntervalTypeEntity>();
            return new DatabaseResponse<TimeIntervalType>(DatabaseResponse<TimeIntervalType>.ResponseCode.Success, tit.Select(ToTimeIntervalType).ToList());
        }

		#region Converters
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
                Id = entity.Id,
                TimeIntervalType = ToTimeIntervalType(intervalType),
                Begin = entity.StartDate,
                End = entity.EndDate,
                Comment = entity.Comment ?? ""
            };
        }

        private TimeIntervalType ToTimeIntervalType(TimeIntervalTypeEntity entity)
        {
            return new TimeIntervalType()
            {
                Id = entity.Id,
                LongName = entity.Name,
                ShortName = entity.ShortName,
                Description = entity.Description,
            };
        }
		private TimeIntervalEntity ToTimeIntervalEntity(TimeInterval ti, int employeeId)
		{
			return new TimeIntervalEntity()
			{
                Id = ti.Id,
				StartDate = ti.Begin,
                EndDate = ti.End,
                Comment = ti.Comment,
                IntervalTypeId = ti.TimeIntervalType.Id,
                EmployeeId = employeeId
			};
		}
		#endregion

		#region Async Methods
		public Task<DatabaseResponse<Employee>> GetEmployeesAsync(int departmentId = 0)
        {
            return Task.FromResult(GetEmployees(departmentId));
        }

        public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(Employee employee)
        {
            return Task.FromResult(GetEmployeePeriods(employee));
        }

        public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(int employeeId)
        {
            return Task.FromResult(GetEmployeePeriods(employeeId));

        }
        #endregion
    }
}
