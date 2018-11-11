using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class GenerateUserAuthenticatorKeyCommandHandler : IRequestHandler<GenerateUserAuthenticatorKeyCommand, GenerateUserAuthenticatorKeyCommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public GenerateUserAuthenticatorKeyCommandHandler(
            IMediator mediator, 
            ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _mediator = mediator;
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<GenerateUserAuthenticatorKeyCommandResponse> Handle(GenerateUserAuthenticatorKeyCommand command, CancellationToken token)
        {
            var getAuthenticatorKeyQueryResult = await _mediator.Send(new GetUserAuthenticatorKeyQuery()
            {
                User = command.User
            });

            if (!string.IsNullOrEmpty(getAuthenticatorKeyQueryResult.AuthenticatorKey))
            {
                var resetAuthenticatorKeyCommandResponse = await _mediator.Send(new ResetUserAuthenticatorKeyCommand()
                {
                    User = command.User
                });

                if (!resetAuthenticatorKeyCommandResponse.Succeeded)
                    throw new ApplicationException();
            }

            var authenticatorKey = await _twoFactorAuthenticationService.GetUserAuthenticatorKey(command.User);

            return new GenerateUserAuthenticatorKeyCommandResponse()
            {
                 AuthenticatorKey = authenticatorKey
            };
        }
    }
}