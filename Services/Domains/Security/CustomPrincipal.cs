using System;
using System.Linq;
using System.Security.Principal;

namespace Core.Security
{
	public class CustomPrincipal : IPrincipal
	{
		public CustomPrincipal()
		{

		}

		public CustomPrincipal(UserIdentity identity)
		{
			this.Identity = identity;
		}

		private UserIdentity identity;

		public UserIdentity Identity { get => identity ?? new AnonymousIdentity(); set { identity = value; } }
		IIdentity IPrincipal.Identity => Identity;

		public bool IsInRole(string role)
		{
			return identity.Roles.Contains(role);
		}
	}
}
