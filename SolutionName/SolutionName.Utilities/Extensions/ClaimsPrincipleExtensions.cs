using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SolutionName.Utilities.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static List<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            var first = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;
            var last = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value;
            return $"{first} {last}";
        }
    }
}
