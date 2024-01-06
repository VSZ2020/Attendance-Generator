using Services.Domains;

namespace Services.Session
{
	public class SessionService
	{
		public static UserAccount? User { get; set; }
		public static bool IsLoggedIn { get => User != null; }

		public static Guid? CurrentEstablishemntId { get; set; }
	}
}
