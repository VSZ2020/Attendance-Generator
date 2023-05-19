using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Logger
{
    public interface ILogger
    {
        void Log(string Message, string? header = null);
        void Log(Exception exception, string? header = null);
    }
}
