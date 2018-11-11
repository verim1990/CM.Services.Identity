using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class DisableUserTwoFactorAuthenticationCommandHandler : IRequestHandler<DisableUserTwoFactorAuthenticationCommand, DisableUserTwoFactorAuthenticationCommandResponse>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public DisableUserTwoFactorAuthenticationCommandHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<DisableUserTwoFactorAuthenticationCommandResponse> Handle(DisableUserTwoFactorAuthenticationCommand command, CancellationToken token)
        {
            var result = await _twoFactorAuthenticationService.SetUserTwoFactorEnabled(command.User, false);
            await _twoFactorAuthenticationService.ResetUserAuthenticatorKey(command.User);

            return new DisableUserTwoFactorAuthenticationCommandResponse()
            {
                IdentityResult = result
            };
        }
    }
}