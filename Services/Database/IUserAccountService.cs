using Services.POCO;

namespace Services.Database
{
	public interface IUserAccountService
	{
		public DatabaseResponse<UserAccount> GetUserAccounts();
		public DatabaseResponse<UserAccount> GetUserById(int id);
	}
}
