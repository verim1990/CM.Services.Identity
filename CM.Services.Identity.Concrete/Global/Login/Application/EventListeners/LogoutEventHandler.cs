using CM.Services.Identity.Contract.Global.Login.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.EventListeners
{
    public class LogoutEventHandler : INotificationHandler<LogoutEvent>
    {
        Task INotificationHandler<LogoutEvent>.Handle(LogoutEvent notification, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}