using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SolutionName.Web.Helpers
{
    public class IdentityHelper : IIdentityHelper
    {
        private readonly CognitoUserPool _userPool;
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly UserManager<CognitoUser> _userManager;
        public IdentityHelper(
            CognitoUserPool userPool,
            SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager)
        {
            _userPool = userPool;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<IActionResult> SignInAsync(string email, string password, bool rememberMe)
        {
            throw new NotImplementedException();
        }
    }
}
