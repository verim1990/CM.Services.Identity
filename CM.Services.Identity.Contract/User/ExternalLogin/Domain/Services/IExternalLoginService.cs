using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services
{
    public interface IExternalLoginService
    {
        Task<IEnumerable<UserLoginInfo>> GetExternalLogins();

        Task<SignInResult> ExternalLoginSignIn(string loginProvider, string providerKey, bool isPersistent);
    }
}