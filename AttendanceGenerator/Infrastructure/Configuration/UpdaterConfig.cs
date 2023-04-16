using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Infrastructure.Configuration
{
    public class UpdaterConfig : IConfig
    {
        public string Name => nameof(UpdaterConfig);
    }
}
