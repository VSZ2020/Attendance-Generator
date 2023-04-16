using AttendanceGenerator.Infrastructure.Activation;
using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.Session
{
    public class UserSession
    {
        public bool IsLoggedIn { get; set; } = false;
        public string WorkingPath { get; set; } = String.Empty;
        public IRegistrationData? Registration { get; }
        public UserAccount.UserAccount? Account { get; set; }
        /// <summary>
        /// Время, когда истекает сессия пользователя.
        /// Необходима проверка каждые 30 минут. 
        /// </summary>
        public DateTime SessionTimeExpired { get; set; }

        public Role.Role? AccountRole { get; set; }

        public void TryLoadLastSession()
        {
            
        }

        public void LogInUser(UserAccount.UserAccount acc)
        {
            IsLoggedIn = true;
            Account = acc;
            AccountRole = acc.UserRole;
            SessionTimeExpired = DateTime.Now.AddHours(1);
        }

        public void Logout()
        {
            IsLoggedIn = false;
            Account = null;
            AccountRole = null;
            WorkingPath = String.Empty;
        }
    }
}
