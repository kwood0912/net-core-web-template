using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SolutionName.Web.Controllers
{
    [Authorize(Roles = "Administrators,GlobalAdministrators")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            return View(roles);
        }
    }
}