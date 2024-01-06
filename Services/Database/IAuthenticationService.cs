using Core.Database.AppEntities;
using Microsoft.EntityFrameworkCore;
using Services.Domains;

namespace Services.Database
{
	/// <summary>
	/// Сервис аутентификации пользователей
	/// </summary>
    public interface IAuthenticationService
	{
		/// <summary>
		/// Аутентифицирует пользователя по логину и паролю
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		UserAccount? AuthenticateUser(string login, string password);

        Task<UserAccount?> AuthenticateUserAsync(string login, string password);
    }

	public class DefaultAuthentificationService : BaseDatabaseService<SQLiteRepository.AppContext>, IAuthenticationService
    {
		public UserAccount? AuthenticateUser(string login, string password)
		{
            var pass_hash = GetPassHash(login, password);
            var user = Context.Set<UserAccountEntity>().FirstOrDefault(u => u.UserName == login && u.PasswordHash == pass_hash);
			if (user is null)
				return null;

			return new UserAccount(user) { SessionExpiredAt = DateTime.Now.AddHours(8) };
		}

        public async Task<UserAccount?> AuthenticateUserAsync(string login, string password)
        {
            var pass_hash = GetPassHash(login, password);
            var user = await Context.Set<UserAccountEntity>().FirstOrDefaultAsync(u => u.UserName == login && u.PasswordHash == password);
            if (user is null)
                return null;

            return new UserAccount(user) { SessionExpiredAt = DateTime.Now.AddHours(8) };
        }

		private string GetPassHash(string username, string pass)
		{
            //TODO: Calculate password hash
            return pass;
		}
    }
}
