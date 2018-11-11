using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.ExternalLogin.Domain.Services
{
    public class UserExternalLoginService : IUserExternalLoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserExternalLoginService(
            IMediator mediator, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<UserLoginInfo>> GetUserExternalLogins(ApplicationUser user)
        {
            return await _userManager.GetLoginsAsync(user);
        }

        public async Task<ExternalLoginInfo> GetUserExternalLoginInfo(ApplicationUser user = null)
        {
            return await _signInManager.GetExternalLoginInfoAsync(user?.Id.ToString());
        }

        public async Task<IdentityResult> AddUserExternalLogin(ApplicationUser user, ExternalLoginInfo info)
        {
            return await _userManager.AddLoginAsync(user, info);
        }

        public async Task<IdentityResult> RemoveUserExternalLogin(ApplicationUser user, string loginProvider, string providerKey)
        {
            return await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        }
    }
}