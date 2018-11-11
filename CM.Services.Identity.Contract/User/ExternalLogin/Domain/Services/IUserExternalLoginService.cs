using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services
{
    public interface IUserExternalLoginService
    {
        Task<IEnumerable<UserLoginInfo>> GetUserExternalLogins(ApplicationUser user);

        Task<ExternalLoginInfo> GetUserExternalLoginInfo(ApplicationUser user = null);

        Task<IdentityResult> AddUserExternalLogin(ApplicationUser user, ExternalLoginInfo info);

        Task<IdentityResult> RemoveUserExternalLogin(ApplicationUser user, string loginProvider, string providerKey);
    }
}