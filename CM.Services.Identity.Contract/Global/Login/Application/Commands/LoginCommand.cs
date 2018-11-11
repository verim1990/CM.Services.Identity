using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Login.Application.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}