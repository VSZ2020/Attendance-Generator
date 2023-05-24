using Services.POCO;

namespace Services.Database
{
	public interface IEmployeeService
	{
		public DatabaseResponse<Employee> GetEmployees(int departmentId = 0);
		public DatabaseResponse<TimeInterval> GetEmployeePeriods(Employee employee);
	}
}
