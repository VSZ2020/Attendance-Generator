using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.UserPreferences
{
    public class AccountConfig : IConfig
    {
        public UIConfig UIAccountConfig { get; set; }
    }
}
