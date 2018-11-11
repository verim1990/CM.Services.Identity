using CM.Services.Identity.Contract.Global.Register.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Register.Application.EventListeners
{
    public class RegisterEventHandler : INotificationHandler<RegisterEvent>
    {
        Task INotificationHandler<RegisterEvent>.Handle(RegisterEvent notification, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}