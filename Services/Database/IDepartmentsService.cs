using Services.POCO;

namespace Services.Database
{
	public interface IDepartmentsService
	{
		public DatabaseResponse<Department> GetDepartments(UserAccount user, bool countEmployees = false);

		public void AddDepartment(Department? dep);
		public void RemoveDepartment(Department? dep);
		public void RemoveDepartment(int id);

		public Task<DatabaseResponse<Department>> GetDepartmentsAsync(UserAccount user, bool countEmployees = false);
	}
}
