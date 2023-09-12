using Services.Domains;

namespace Services.Database
{
	public class SecureDepartmentService: IDepartmentsService
	{
		#region ctor
		public SecureDepartmentService(UserAccount user)
		{
			this.user = user;
		}
		#endregion

		private readonly UserAccount user;

		public DatabaseResponse<Department> GetDepartments(bool countEmployees = false)
		{
			throw new NotImplementedException();
		}

		public void AddDepartment(Department? dep)
		{
			throw new NotImplementedException();
		}

		public void RemoveDepartment(Department? dep)
		{
			throw new NotImplementedException();
		}

		public void RemoveDepartment(int id)
		{
			throw new NotImplementedException();
		}

		public Task<DatabaseResponse<Department>> GetDepartmentsAsync(bool countEmployees = false)
		{
			throw new NotImplementedException();
		}
	}
}
