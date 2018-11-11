using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;

namespace CM.Services.Identity.Contract.User.Password.Application.Commands.Responses
{
    public class ForgetUserPasswordResponse
    {
        public string Token { get; set; }
    }
}