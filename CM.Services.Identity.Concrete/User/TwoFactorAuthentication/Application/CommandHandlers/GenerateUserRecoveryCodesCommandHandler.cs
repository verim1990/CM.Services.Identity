using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class GenerateUserRecoveryCodesCommandHandler : IRequestHandler<GenerateUserRecoveryCodesCommand, GenerateUserRecoveryCodesCommandResponse>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public GenerateUserRecoveryCodesCommandHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<GenerateUserRecoveryCodesCommandResponse> Handle(GenerateUserRecoveryCodesCommand command, CancellationToken token)
        {
            var recoveryCodes = await _twoFactorAuthenticationService.GenerateNewUserTwoFactorRecoveryCodes(command.User, command.Count);

            return new GenerateUserRecoveryCodesCommandResponse()
            {
                 RecoveryCodes = recoveryCodes.ToList()
            };
        }
    }
}