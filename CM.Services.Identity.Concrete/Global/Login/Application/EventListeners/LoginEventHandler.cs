using CM.Services.Identity.Contract.Global.Login.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.EventListeners
{
    public class LoginEventHandler : INotificationHandler<LoginEvent>
    {
        public Task Handle(LoginEvent notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}