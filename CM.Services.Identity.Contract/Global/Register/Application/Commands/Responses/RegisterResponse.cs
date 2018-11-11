using CM.Services.Identity.Contract.Global.Register.Domain.Models;

namespace CM.Services.Identity.Contract.Global.Register.Application.Commands.Responses
{
    public class RegisterResponse
    {
        public RegisterResult RegisterResult { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
    }
}