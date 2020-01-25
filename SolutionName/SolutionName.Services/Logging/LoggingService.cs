using System;
using System.Collections.Generic;
using System.Text;
using SolutionName.Data.Repositories;
using SolutionName.Models.Database;

namespace SolutionName.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogDatabaseRepository _logRepository;
        public LoggingService(ILogDatabaseRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void LogEvent(LogType logType, string description, Exception exception)
        {
            _logRepository.Insert(new Log() 
            { 
                LogType = logType.ToString(),
                Code = $"{exception.TargetSite.DeclaringType.Name}.{exception.TargetSite.Name}",
                Description = description,
                Message = exception.Message,
                StackTrace = exception.StackTrace
            });
        }

        public void LogEvent(LogType logType, string code, string description, Exception exception)
        {
            _logRepository.Insert(new Log()
            {
                LogType = logType.ToString(),
                Code = code,
                Description = description,
                Message = exception.Message,
                StackTrace = exception.StackTrace
            });
        }

        public void LogEvent(LogType logType, string code, string description)
        {
            _logRepository.Insert(new Log()
            {
                LogType = logType.ToString(),
                Code = code,
                Description = description
            });
        }

        public void LogEvent(Exception exception)
        {
            _logRepository.Insert(new Log()
            {
                LogType = LogType.ERROR.ToString(),
                Code = "EXCEPTION",
                Message = exception.Message,
                StackTrace = exception.StackTrace
            });
        }
    }
}
