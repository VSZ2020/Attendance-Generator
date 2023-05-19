using Core.Database.AppEntities;
using Core.Database.Entities;
using Core.Security;
using Services.POCO;
using SQLiteRepository;

namespace Services.Database
{
    public class UserAccountService
    {
        /// <summary>
        /// Базовый конструктор сервиса
        /// </summary>
        public UserAccountService(IItemsRepository<SQLiteRepository.AppContext> usersRep)
        {
            this.usersRepository = usersRep;
        }


        private readonly IItemsRepository<SQLiteRepository.AppContext> usersRepository;

        public virtual DatabaseResponse<UserAccount> GetUserAccounts(UserRoleType role)
        {
            if (usersRepository == null || !UserRole.HasAccess(role, PermissionType.ViewUserAccounts))
                return new DatabaseResponse<UserAccount>(
                    DatabaseResponse<UserAccount>.ResponseCode.PermissionsError,
                    new List<UserAccount>(),
                    "Нет прав на просмотр списка пользователей");

            IList<UserAccountEntity> userAccounts;
            if (UserRole.HasAccess(role, PermissionType.ViewAllUserAccounts))
                userAccounts = usersRepository.GetAll<UserAccountEntity>();
            else
                return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.PermissionsError, new List<UserAccount>() { });

            return new DatabaseResponse<UserAccount>(DatabaseResponse<UserAccount>.ResponseCode.Success, userAccounts.Select(DbEntityToUserAccount).ToList());
        }

        public virtual DatabaseResponse<UserAccount> GetUserById(int id)
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
                RoleType = entity.Role
            };
        }

        
        #endregion
    }
}
