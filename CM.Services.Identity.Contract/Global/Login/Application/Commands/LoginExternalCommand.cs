using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Login.Application.Commands
{
    public class LoginExternalCommand : IRequest<LoginExternalResponse>
    {
    }
}