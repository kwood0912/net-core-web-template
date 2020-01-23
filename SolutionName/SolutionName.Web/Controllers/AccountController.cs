using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolutionName.Web.Models;

namespace SolutionName.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly CognitoUserPool _userPool;
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly CognitoUserManager<CognitoUser> _userManager;

        public AccountController(
            CognitoUserPool userPool,
            SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager)
        {
            _userPool = userPool;
            _signInManager = signInManager;
            _userManager = userManager as CognitoUserManager<CognitoUser>;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            IActionResult result = View();
            if (ModelState.IsValid)
            {
                var user = _userPool.GetUser(model.Email);
                user.Attributes.Add(CognitoAttribute.Email.AttributeName, model.Email);
                user.Attributes.Add(CognitoAttribute.GivenName.AttributeName, model.FirstName);
                user.Attributes.Add(CognitoAttribute.FamilyName.AttributeName, model.LastName);
                user.Attributes.Add(CognitoAttribute.Name.AttributeName, $"{model.FirstName} {model.LastName}");
                user.Attributes.Add(CognitoAttribute.PhoneNumber.AttributeName, $"+1{model.PhoneNumber}");
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    result = RedirectToAction(nameof(Confirm));
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return result;
        }

        [HttpGet("confirm")]
        public IActionResult Confirm()
        {
            return View();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm(AccountConfirmViewModel model)
        {
            IActionResult result = View(model);
            if (ModelState.IsValid)
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var confirmResult = await _userManager.ConfirmSignUpAsync(user, model.ConfirmationCode, true);
                    if (confirmResult.Succeeded)
                    {
                        result = RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in confirmResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return result;
        }

        [HttpGet("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}