using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer
{
    public class clsEventLog
    {
        private const string _Source = "CarWash";
        private const string _LogName = "Application";

        public static void LogException(string OperationName, Exception ex)
        {

            try
            {
                if (!EventLog.SourceExists(_Source))
                {
                    EventLog.CreateEventSource(_Source, _LogName);
                }
                string message = $@"Date: {DateTime.Now}
Operation: {OperationName}
Exception Type: {ex.GetType().Name}
Message: {ex.Message}
Source: {ex.Source}
Stack Trace:
{ex.StackTrace}";

                EventLog.WriteEntry(
                    _Source,
                    message,
                    EventLogEntryType.Error);
            }
            catch { }
            ;

        }
    }
}
