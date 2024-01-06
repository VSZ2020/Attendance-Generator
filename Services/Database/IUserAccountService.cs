using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;
using Services.Domains;

namespace Services.Database
{
	#region IUserAccountService
	public interface IUserAccountService
	{
		public Task<IList<UserAccount>> GetUserAccountsAsync(FetchAim aim = FetchAim.None);
		public Task<UserAccount> GetUserAccountByIdAsync(Guid id, FetchAim aim = FetchAim.None);
		public Task<bool> AddUserAccountAsync(UserAccount userAccount);
		public Task<bool> RemoveUserAccountAsync(Guid id);
		public Task<bool> UpdateUserAccountAsync(UserAccount newUserAccount);
	}
	#endregion

	#region DefaultUserAccountService
	public class DefaultUserAccountService : BaseDatabaseService<SQLiteRepository.AppContext>, IUserAccountService
	{
		#region ctor
		public DefaultUserAccountService(IDepartmentsService departmentService)
		{
			this.departmentService = departmentService;
		}
		#endregion

		private IDepartmentsService departmentService;

		#region GetUserAccountByIdAsync
		public async Task<UserAccount> GetUserAccountByIdAsync(Guid id, FetchAim aim = FetchAim.None)
		{
			var entity = await Context.Set<UserAccountEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id) ?? new UserAccountEntity();
			var department = (aim == FetchAim.Card || aim == FetchAim.Table) ? await departmentService.GetDepartmentByIdAsync(entity.DepartmentId, FetchAim.Table) : null;
			return new UserAccount(entity)
			{
				Department = department,
			};
		}
		#endregion

		#region GetUserAccountsAsync
		public async Task<IList<UserAccount>> GetUserAccountsAsync(FetchAim aim = FetchAim.None)
		{
			var entitiesIds =
				await Context
				.Set<UserAccountEntity>()
				.AsNoTracking().Select(e => e.Id)
				.ToListAsync();
			return entitiesIds
				.Select(id => GetUserAccountByIdAsync(id, aim).Result)
				.ToList();
		}
		#endregion

		#region AddUserAccountAsync
		public Task<bool> AddUserAccountAsync(UserAccount userAccount)
		{
			return base.AddAsync<UserAccount, UserAccountEntity>(userAccount);
		}
		#endregion

		#region RemoveUserAccountAsync
		public Task<bool> RemoveUserAccountAsync(Guid id)
		{
			return base.DeleteById<UserAccount, UserAccountEntity>(id);
		}
		#endregion

		#region UpdateUserAccountAsync
		public Task<bool> UpdateUserAccountAsync(UserAccount newUserAccount)
		{
			return base.Update<UserAccount, UserAccountEntity>(newUserAccount);
		}
		#endregion
	} 
	#endregion
}
