using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.TwoFactorAuthentication.Domain.Services
{
    public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TwoFactorAuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserAuthenticatorKey(ApplicationUser user)
        {
            return await _userManager.GetAuthenticatorKeyAsync(user);
        }

        public async Task<int> GetUserRecoveryCodesCount(ApplicationUser user)
        {
            return await _userManager.CountRecoveryCodesAsync(user);
        }

        public async Task ResetUserAuthenticatorKey(ApplicationUser user)
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
        }

        public async Task<IdentityResult> SetUserTwoFactorEnabled(ApplicationUser user, bool enabled)
        {
            return await _userManager.SetTwoFactorEnabledAsync(user, enabled);
        }

        public async Task<bool> VerifyUserTwoFactorToken(ApplicationUser user, string verificationCode)
        {
            var tokenProvider = _userManager.Options.Tokens.AuthenticatorTokenProvider;

            return await _userManager.VerifyTwoFactorTokenAsync(user, tokenProvider, verificationCode);
        }

        public async Task<IEnumerable<string>> GenerateNewUserTwoFactorRecoveryCodes(ApplicationUser user, int count)
        {
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, count);

            return recoveryCodes;
        }
    }
}