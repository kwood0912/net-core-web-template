using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.Services.Identity
{
    public interface IIdentityService
    {
        Task<bool> AssignRole(string email, string role);
        Task<bool> UnassignRole(string email, string role);
    }
}
