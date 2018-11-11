using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Contract.Global.Login.Domain.Models;
using CM.Services.Identity.Contract.Global.Login.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(
            IMediator mediator,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResult> Login(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return new LoginResult()
                {
                    SignInResult = SignInResult.Failed
                };

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!isEmailConfirmed)
                return new LoginResult()
                {
                    SignInResult = SignInResult.NotAllowed,
                    EmailNotConfirmed = true
                };

            var signInResult = await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);

            return new LoginResult()
            {
                SignInResult = signInResult
            };
        }

        public async Task<SignInResult> LoginWithTwoFactorAuthentication(string authenticatorCode, bool rememberMe, bool rememberMachine)
        {
            return await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, rememberMachine);
        }

        public async Task<SignInResult> LoginWithRecoveryCode(string recoveryCode)
        {
            return await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

            return;
        }
    }
}