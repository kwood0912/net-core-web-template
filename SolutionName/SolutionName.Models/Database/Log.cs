using Dapper;
using SolutionName.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Models.Database
{
    [Table("logs")]
    public class Log : DatabaseModel
    {
        public int LogId { get; set; }
        public string LogType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }

    public enum LogType
    {
        INFORMATION,
        WARNING,
        ERROR
    }
}
