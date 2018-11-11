using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class EnableUserTwoFactorAuthenticationCommandHandler : IRequestHandler<EnableUserTwoFactorAuthenticationCommand, EnableUserTwoFactorAuthenticationCommandResponse>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public EnableUserTwoFactorAuthenticationCommandHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<EnableUserTwoFactorAuthenticationCommandResponse> Handle(EnableUserTwoFactorAuthenticationCommand command, CancellationToken token)
        {
            var result = await _twoFactorAuthenticationService.SetUserTwoFactorEnabled(command.User, true);
            
            return new EnableUserTwoFactorAuthenticationCommandResponse()
            {
                IdentityResult = result
            };
        }
    }
}