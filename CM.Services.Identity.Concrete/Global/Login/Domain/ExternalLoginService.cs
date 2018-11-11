using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Domain.Services
{
    public class ExternalLoginService : IExternalLoginService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExternalLoginService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<UserLoginInfo>> GetExternalLogins()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Select(scheme => new UserLoginInfo(scheme.Name, scheme.Name, scheme.DisplayName));
        }

        public async Task<SignInResult> ExternalLoginSignIn(string loginProvider, string providerKey, bool isPersistent)
        {
            return await _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }
    }
}