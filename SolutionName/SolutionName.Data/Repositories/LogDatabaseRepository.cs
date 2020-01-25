using Microsoft.Extensions.Configuration;
using SolutionName.Data.Common;
using SolutionName.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Data.Repositories
{
    public class LogDatabaseRepository : DatabaseRepository<Log>, ILogDatabaseRepository
    {
        public LogDatabaseRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
