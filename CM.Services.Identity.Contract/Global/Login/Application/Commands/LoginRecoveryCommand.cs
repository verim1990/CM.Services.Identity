using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Login.Application.Commands
{
    public class LoginRecoveryCommand : IRequest<LoginResponse>
    {
        public string RecoveryCode { get; set; }
    }
}