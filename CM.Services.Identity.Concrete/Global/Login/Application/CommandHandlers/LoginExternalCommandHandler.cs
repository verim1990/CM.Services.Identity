using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Login.Domain.Models;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class LoginExternalCommandHandler : IRequestHandler<LoginExternalCommand, LoginExternalResponse>
    {
        private readonly IExternalLoginService _externalLoginService;
        private readonly IUserExternalLoginService _userExternalLoginService;

        public LoginExternalCommandHandler(
            IExternalLoginService externalLoginService,
            IUserExternalLoginService userExternalLoginService)
        {
            _externalLoginService = externalLoginService;
            _userExternalLoginService = userExternalLoginService;
        }

        public async Task<LoginExternalResponse> Handle(LoginExternalCommand command, CancellationToken token)
        {
            var info = await _userExternalLoginService.GetUserExternalLoginInfo();

            if (info == null)
            {
                return new LoginExternalResponse()
                {
                    ExternalLoginInfo = null
                };
            }

            var result = await _externalLoginService.ExternalLoginSignIn(info.LoginProvider, info.ProviderKey, isPersistent: false);

            return new LoginExternalResponse()
            {
                ExternalLoginInfo = info,
                LoginResult = new LoginResult()
                {
                    SignInResult = result
                }
            };
        }
    }
}