using Core.Converters;
using Core.Database.AppEntities;

namespace Services.Domains
{
	public class UserAccount : BaseDomain, IEntityConverter<UserAccountEntity>
	{
		#region ctor
		public UserAccount(Guid id, string username, string login, string passwd_hash, Guid departmentId, string[] roles)
		{
			Id = id;
			Username = username;
			Login = login;
			this.DepartmentId = departmentId;
			this.Roles = roles;
		}

		public UserAccount(UserAccountEntity entity)
		{
			Id = entity.Id;
			Username = entity.UserName;
			Login = entity.Login;
			Roles = entity.Roles;
			DepartmentId = entity.DepartmentId;
			SessionExpiredAt = entity.SessionExpiredAt;
		}
		#endregion

		public string Username { get; set; }
		public string Login { get; set; }
		public DateTime SessionExpiredAt { get; set; }

		public string[] Roles { get; set; }
		public Guid DepartmentId { get; set; }
		public Department? Department { get; set; }
		

		#region IEntityConverter
		public UserAccountEntity ConvertToEntity()
		{
			return new UserAccountEntity()
			{
				Id = Id,
				UserName = Username,
				Login = Login,
				Roles = Roles,
				DepartmentId = DepartmentId,
				SessionExpiredAt = SessionExpiredAt,

			};
		}
		#endregion
	}
}
