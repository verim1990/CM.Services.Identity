using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.Password.Application.Commands.Responses
{
    public class ResetUserPasswordResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}