using Microsoft.Extensions.Configuration;
using SolutionName.Data.Common;
using SolutionName.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolutionName.Data.Repositories
{
    public class UserDatabaseRepository : DatabaseRepository<User>, IUserDatabaseRepository
    {
        public UserDatabaseRepository(IConfiguration configuration) : base(configuration)
        {
        }


    }
}
