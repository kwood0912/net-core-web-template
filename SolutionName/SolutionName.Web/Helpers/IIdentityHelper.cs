using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Web.Helpers
{
    public interface IIdentityHelper
    {
        public Task<IActionResult> SignInAsync(string email, string password, bool rememberMe);
    }
}
