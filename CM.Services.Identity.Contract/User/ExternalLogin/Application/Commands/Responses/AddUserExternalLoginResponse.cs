using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses
{
    public class AddUserExternalLoginResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}