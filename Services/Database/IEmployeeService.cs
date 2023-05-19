using Services.POCO;

namespace Services.Database
{
	public interface IEmployeeService
	{
		public DatabaseResponse<Employee> GetEmployees(UserAccount user, int departmentId = 0);
	}
}
