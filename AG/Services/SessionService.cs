using Services.Domains;

namespace AG.Services
{
	public class SessionService
    {
        public static UserAccount? User { get; set; }
        public static bool IsLoggedIn { get => User != null; }

    }
}
