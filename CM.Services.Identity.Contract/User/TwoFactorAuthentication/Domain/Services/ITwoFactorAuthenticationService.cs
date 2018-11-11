using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services
{
    public interface ITwoFactorAuthenticationService
    {
        Task<string> GetUserAuthenticatorKey(ApplicationUser user);

        Task ResetUserAuthenticatorKey(ApplicationUser user);

        Task<int> GetUserRecoveryCodesCount(ApplicationUser user);

        Task<bool> VerifyUserTwoFactorToken(ApplicationUser user, string verificationCode);

        Task<IdentityResult> SetUserTwoFactorEnabled(ApplicationUser user, bool test);

        Task<IEnumerable<string>> GenerateNewUserTwoFactorRecoveryCodes(ApplicationUser user, int count);
    }
}