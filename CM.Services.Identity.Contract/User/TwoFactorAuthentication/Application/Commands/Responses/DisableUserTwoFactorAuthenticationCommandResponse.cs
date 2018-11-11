using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses
{
    public class DisableUserTwoFactorAuthenticationCommandResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}