using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Application.Events;
using CM.Services.Identity.Contract.Global.Login.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class LogoutCommandHandler : AsyncRequestHandler<LogoutCommand>
    {
        private readonly IMediator _mediator;
        private readonly ILoginService _loginService;

        public LogoutCommandHandler(ILoginService loginService, IMediator mediator)
        { 
            _loginService = loginService;
            _mediator = mediator;
        }

        protected async override Task Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            await _loginService.Logout();
            await _mediator.Publish(new LogoutEvent());

            return;
        }
    }
}