using Core.Database.AppEntities;
using Core.Database.Entities;
using Services.Domains;
using SQLiteRepository;

namespace Services.Database
{
	public class UserAccountService: IUserAccountService
    {
		#region ctor
		/// <summary>
		/// Базовый конструктор сервиса
		/// </summary>
		public UserAccountService(IAppItemsRepository usersRep)
        {
            this.usersRepository = usersRep;
        }
        #endregion ctor

        #region fields
        private readonly IAppItemsRepository usersRepository;

		#endregion fields

		public DatabaseResponse<UserAccount> GetUserAccounts()
        {
            var userAccounts = usersRepository.GetAll<UserAccountEntity>();
            
            return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.Success, userAccounts.Select(DbEntityToUserAccount).ToList());
        }

        public DatabaseResponse<UserAccount> GetUserById(int id)
        {
            var user = DbEntityToUserAccount(usersRepository.GetById<UserAccountEntity>(id));
            return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.Success,new List<UserAccount>() { user } );
        }

        #region Utils
        protected EmployeeFunction DbEntityToFunction(FunctionEntity? entity)
        {
            return entity == null ?
                new EmployeeFunction() { Id = 0, Name = "Не определена", ShortName = "Неопр."} :
                new EmployeeFunction() { Id = entity.Id, Name = entity.Name, ShortName = entity.ShortName };
        }

        protected EmployeeStatus DbEntityToStatus(EmployeeStatusEntity? entity)
        {
            return entity == null ?
                new EmployeeStatus() { Id = 0, Name = "Не определен" } :
                new EmployeeStatus() { Id = entity.Id, Name = entity.Name };
        }

        protected UserAccount DbEntityToUserAccount(UserAccountEntity entity)
        {
            return new UserAccount()
            {
                Id = entity.Id,
                Username = entity.UserName,
                Login = entity.Login,
                SessionExpiredAt = entity.SessionExpiredAt,
                DepartmentId = entity.DepartmentId.HasValue ? entity.DepartmentId.Value : 0,
                Roles = entity.Roles
            };
        }

        
        #endregion
    }
}
