using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutionName.Utilities.Extensions;
using SolutionName.Web.Models;

namespace SolutionName.Web.Controllers
{
    [Authorize(Roles = "Administrators,GlobalAdministrators")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var avm = new AdminViewModel()
            {
                Email = User.GetEmail(),
                Roles = User.GetRoles(),
                Name = User.GetName()
            };
            return View(avm);
        }
    }
}