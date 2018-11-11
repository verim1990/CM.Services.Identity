using CM.Services.Identity.Contract.User.Email.Application.Commands;
using CM.Services.Identity.Contract.Global.Register.Application.Commands;
using CM.Services.Identity.Contract.Global.Register.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Register.Domain.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace CM.Services.Identity.Concrete.Global.Register.Application.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRegisterService _registerService;

        public RegisterCommandHandler(IRegisterService registerService, IMediator mediator)
        {
            _registerService = registerService;
            _mediator = mediator;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken token)
        {
            var userEmailConfirmationToken = string.Empty;
            var registerResult = await _registerService.Register(command.Username, command.Email, command.Password);

            if (registerResult.IdentityResult.Succeeded)
            {
                var generateUserEmailConfirmationTokenResult = await _mediator.Send(new GenerateUserEmailConfirmationTokenCommand()
                {
                    User = registerResult.User
                });

                userEmailConfirmationToken = generateUserEmailConfirmationTokenResult.Token;
            }
            
            return new RegisterResponse()
            {
                RegisterResult = registerResult,
                Token = userEmailConfirmationToken
            };
        }
    }
}