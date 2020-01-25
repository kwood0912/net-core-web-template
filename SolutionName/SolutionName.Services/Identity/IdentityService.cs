using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using SolutionName.Models.Database;
using SolutionName.Services.Logging;
using SolutionName.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly CognitoUserPool _userPool;
        private readonly SignInManager<CognitoUser> _signInManager;
        private readonly CognitoUserManager<CognitoUser> _userManager;
        private readonly ILoggingService _loggingService;

        public IdentityService(
            CognitoUserPool userPool,
            SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager,
            ILoggingService loggingService)
        {
            _userPool = userPool;
            _signInManager = signInManager;
            _userManager = userManager as CognitoUserManager<CognitoUser>;
            _loggingService = loggingService;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            bool result = false;
            try 
            {
                var cogUser = await _userManager.FindByEmailAsync(email);
                var identityResult = await _userManager.AddToRoleAsync(cogUser, role);
                result = identityResult.Succeeded;
                if (identityResult.Errors?.Any() ?? false)
                {
                    _loggingService.LogEvent(
                        LogType.WARNING,
                        $"{nameof(IdentityService)}.{nameof(AssignRole)}",
                        string.Join("\n", identityResult.Errors.Select(e => $"{e.Code} - {e.Description}")));
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogEvent(ex);
            }
            return result;
        }

        public async Task<bool> UnassignRole(string email, string role)
        {
            bool result = false;
            try
            {
                var cogUser = await _userManager.FindByEmailAsync(email);
                var identityResult = await _userManager.RemoveFromRoleAsync(cogUser, role);
                result = identityResult.Succeeded;
                if (identityResult.Errors?.Any() ?? false)
                {
                    _loggingService.LogEvent(
                        LogType.WARNING,
                        $"{nameof(IdentityService)}.{nameof(AssignRole)}",
                        string.Join("\n", identityResult.Errors.Select(e => $"{e.Code} - {e.Description}")));
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogEvent(ex);
            }
            return result;
        }
    }
}
