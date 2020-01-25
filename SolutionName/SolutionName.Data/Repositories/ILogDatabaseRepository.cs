using SolutionName.Data.Common;
using SolutionName.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Data.Repositories
{
    public interface ILogDatabaseRepository : IDatabaseRepository<Log>
    {
    }
}
