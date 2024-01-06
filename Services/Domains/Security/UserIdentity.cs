using Core.Database.AppEntities;
using Services.Domains;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Core.Security
{
	public class UserIdentity : IIdentity
	{
		public UserIdentity(UserAccount? account)
		{
			this.account = account;
		}


		private UserAccount? account;
		public string AuthenticationType => typeof(UserAccountEntity).ToString();

		public bool IsAuthenticated => account != null && !string.IsNullOrEmpty(account.Username);

		public string Name => account?.Username ?? string.Empty;

		public Guid? UserDepartmentId { get; set; }

		public string[] Roles => account?.Roles ?? new string[] {};
	}

	public class AnonymousIdentity : UserIdentity
	{
		public AnonymousIdentity(): base(null)
		{

		}
	}
}
