using Services.Domains;

namespace Services.Database
{
	public interface IEmployeeService
	{
		public DatabaseResponse<Employee> GetEmployees(int departmentId = 0);
		public Task<DatabaseResponse<Employee>> GetEmployeesAsync(int departmentId = 0);
		public DatabaseResponse<TimeInterval> GetEmployeePeriods(Employee employee);
		public DatabaseResponse<TimeInterval> GetEmployeePeriods(int employeeId);
		public void AddEmployeePeriod(int employeeId, TimeInterval timeInterval);
		public void UpdateEmployeePeriod(int employeeId, TimeInterval timeInterval);
		public void DeleteEmployeePeriod(int employeeId, TimeInterval timeInterval);
		public void DeleteEmployeePeriod(int timeIntervalId);

		public DatabaseResponse<TimeIntervalType> GetTimeIntervalTypes();
		public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(Employee employee);
		public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(int employeeId);
	}
}
