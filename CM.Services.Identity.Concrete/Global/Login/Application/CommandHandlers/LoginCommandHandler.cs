using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Login.Application.Events;
using CM.Services.Identity.Contract.Global.Login.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IMediator _mediator;
        private readonly ILoginService _loginService;

        public LoginCommandHandler(ILoginService loginService, IMediator mediator)
        {
            _loginService = loginService;
            _mediator = mediator;
        }

        public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken token)
        {
            var loginResult = await _loginService.Login(command.Username, command.Password, command.RememberMe);

            if (loginResult.SignInResult.Succeeded)
                await _mediator.Publish(new LoginEvent());

            return new LoginResponse()
            {
                LoginResult = loginResult
            };
        }
    }
}