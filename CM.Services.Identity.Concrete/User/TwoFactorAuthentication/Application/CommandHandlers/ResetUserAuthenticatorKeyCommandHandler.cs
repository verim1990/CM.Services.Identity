using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class ResetUserAuthenticatorKeyCommandHandler : IRequestHandler<ResetUserAuthenticatorKeyCommand, ResetUserAuthenticatorKeyCommandResponse>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public ResetUserAuthenticatorKeyCommandHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<ResetUserAuthenticatorKeyCommandResponse> Handle(ResetUserAuthenticatorKeyCommand command, CancellationToken token)
        {
            await _twoFactorAuthenticationService.ResetUserAuthenticatorKey(command.User);

            return new ResetUserAuthenticatorKeyCommandResponse()
            {
                Succeeded = true
            };
        }
    }
}