using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses
{
    public class RemoveUserExternalLoginResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}