using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class EnableUserAuthenticatorCommandHandler : IRequestHandler<EnableUserAuthenticatorCommand, EnableUserAuthenticatorCommandResponse>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public EnableUserAuthenticatorCommandHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<EnableUserAuthenticatorCommandResponse> Handle(EnableUserAuthenticatorCommand command, CancellationToken token)
        {
            var isTokenValid = await _twoFactorAuthenticationService.VerifyUserTwoFactorToken(command.User, command.VerificationCode);

            return new EnableUserAuthenticatorCommandResponse()
            {
                Succeeded = isTokenValid,
            };
        }
    }
}