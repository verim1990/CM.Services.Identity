using CM.Services.Identity.Contract.Global.Register.Application.Commands.Responses;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Register.Application.Commands
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}