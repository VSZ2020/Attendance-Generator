using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Activation
{
    public interface IRegistrationData
    {
        bool IsValid { get; }
        DateTime ExpirationDate { get; }
        string LicensedTo { get; }
        string Key { get; }

        bool CheckRegistration();
    }
}
