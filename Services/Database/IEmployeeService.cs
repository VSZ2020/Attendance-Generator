using Services.Domains;

namespace Services.Database
{
	public interface IEmployeeService
	{
		public DatabaseResponse<Employee> GetEmployees(int departmentId = 0);
		public Task<DatabaseResponse<Employee>> GetEmployeesAsync(int departmentId = 0);
		public DatabaseResponse<TimeInterval> GetEmployeePeriods(Employee employee);
		public Task<DatabaseResponse<TimeInterval>> GetEmployeePeriodsAsync(Employee employee);
	}
}
