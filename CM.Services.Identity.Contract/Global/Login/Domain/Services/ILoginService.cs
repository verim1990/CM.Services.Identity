using CM.Services.Identity.Contract.Global.Login.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.Global.Login.Domain.Services
{
    public interface ILoginService
    {
        Task<LoginResult> Login(string username, string password, bool rememberMe);

        Task<SignInResult> LoginWithTwoFactorAuthentication(string authenticatorCode, bool rememberMe, bool rememberMachine);

        Task<SignInResult> LoginWithRecoveryCode(string recoveryCode);

        Task Logout();
    }
}