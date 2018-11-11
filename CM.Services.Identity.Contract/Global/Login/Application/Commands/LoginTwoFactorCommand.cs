using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Login.Application.Commands
{
    public class LoginTwoFactorCommand : IRequest<LoginResponse>
    {
        public string AuthenticatorCode { get; set; }

        public bool RememberMe { get; set; }

        public bool RememberMachine { get; set; }
    }
}