using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolutionName.Web.Extensions;
using SolutionName.Web.Models;

namespace SolutionName.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly CognitoUserManager<CognitoUser> _userManager;

        public AuthenticationController(
            SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager as CognitoUserManager<CognitoUser>;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IActionResult result = View(model);
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (signInResult.Succeeded)
                {
                    result = RedirectToAction("Index", "Home");
                }
                else if (signInResult.RequiresTwoFactor)
                {
                    result = RedirectToAction("Verify");
                }
                else if (signInResult.IsLockedOut)
                {
                    result = RedirectToAction("LockedOut");
                }
                else if (signInResult.IsCognitoSignInResult())
                {
                    if (signInResult is CognitoSignInResult cognitoResult)
                    {
                        if (cognitoResult.RequiresPasswordChange)
                        {
                            result = RedirectToAction("Change", "Password");
                        }
                        else if (cognitoResult.RequiresPasswordReset)
                        {
                            result = RedirectToAction("Reset", "Password");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }
            return result;
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpGet("password/change")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            IActionResult result = View(model);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        TempData.Put("Dialog", new DialogViewModel(DialogType.Success, "Password changed", "Your password has been changed successfully!"));
                        result = RedirectToAction(nameof(ChangePassword));
                    }
                    else
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }
                }
            }
            return result;
        }

        [HttpGet("password/forgot")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("password/forgot")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            IActionResult result = View(model);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    try
                    {
                        await user.ForgotPasswordAsync();
                        result = RedirectToAction(nameof(ResetPassword));
                        TempData.Put("Dialog", new DialogViewModel(DialogType.Success, "Password recovery sent", "A password recovery message was successfully sent to your e-mail inbox."));
                    }
                    catch (Exception)
                    {
                        TempData.Put("Dialog", new DialogViewModel(DialogType.Error, "Password recovery failed", "We were unable to generate a password recovery token for your account.  Please try again."));
                    }
                }
                else
                {
                    TempData.Put("Dialog", new DialogViewModel(DialogType.Error, "Unable to retrieve user", "We were unable to find an account with the e-mail provided.  Please try again."));
                }
            }
            return result;
        }

        [HttpGet("password/recovery")]
        public IActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpGet("password/reset")]
        public IActionResult ResetPassword(string token = null)
        {
            return View(new ResetPasswordViewModel()
            {
                ResetToken = token
            });
        }

        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            IActionResult result = View(model);
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    try
                    {
                        await user.ConfirmForgotPasswordAsync(model.ResetToken, model.NewPassword);
                        result = RedirectToAction(nameof(Login));
                        TempData.Put("Dialog", new DialogViewModel(DialogType.Success, "Password reset", "Your password has been successfully reset."));
                    }
                    catch (Exception ex)
                    {
                        TempData.Put("Dialog", new DialogViewModel(DialogType.Error, "Password recovery failed", ex.Message));
                    }
                }
            }
            return result;
        }
    }
}