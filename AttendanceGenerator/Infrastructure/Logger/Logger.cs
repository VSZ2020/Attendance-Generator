using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Infrastructure.Logger
{
    public static class Logger
    {
        private static string logFilePath;
        private static bool IsEnabled;
        static Logger()
        {
            IsEnabled = true;
            logFilePath = Environment.CurrentDirectory;
            try
            {
                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "logs")))
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "logs"));
                logFilePath = System.IO.Path.Combine(logFilePath, "logs", "debug.log");
            }
            catch(IOException ex)
            {
                logFilePath = System.IO.Path.Combine(logFilePath, "debug.log");
                Log(ex, "Logger IO Error");
            }
            
        }

        public static void Log(string Message, string tag = null)
        {
            if (!IsEnabled) return;
            StringBuilder builder = new StringBuilder();
            if (tag != null)
                tag = String.Format("[{0}]", tag);
            builder.AppendLine(GetDateString() + tag);
            builder.AppendLine("\t" + Message);
            WriteTiFile(builder.ToString());
            Console.WriteLine(builder.ToString());
        }

        public static void Log(Exception ex, string tag=null)
        {
            if (!IsEnabled)
                return;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Target: " + ex.TargetSite);
            builder.AppendLine("\t>> Source: " + ex.Source);
            builder.AppendLine("\t>> Message: " + ex.Message);
            builder.AppendLine("\t>> Stack trace: " + ex.StackTrace);
            if (ex.InnerException != null)
            {
                builder.AppendLine("\t>> Inner Exception: \n\t\tTarget: " + ex.InnerException.TargetSite);
                builder.AppendLine("\t\tSource: " + ex.InnerException.Source);
                builder.AppendLine("\t\tMessage: " + ex.InnerException.Message);
                builder.AppendLine("\t\tMessage: " + ex.InnerException.StackTrace);
            }
            Log(builder.ToString(), tag);
        }

        public static void WriteTiFile(string Message)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath,true))
            {
                writer.WriteLine(Message);
            }
        }

        public static string GetDateString()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }
    }
}
