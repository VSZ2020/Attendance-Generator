using AttendanceGenerator.Model.Session.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.UserPreferences
{
    public class SessionConfig: IConfig
    {
        public UserAccount LastUser { get; set; }
        public DateTime SessionExpiredAt { get; set; }
    }
}
