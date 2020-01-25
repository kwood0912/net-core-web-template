using SolutionName.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Services.Logging
{
    public interface ILoggingService
    {
        void LogEvent(LogType logType, string description, Exception exception);
        void LogEvent(LogType logType, string code, string description, Exception exception);
        void LogEvent(LogType logType, string code, string description);
        void LogEvent(Exception exception);
    }
}
