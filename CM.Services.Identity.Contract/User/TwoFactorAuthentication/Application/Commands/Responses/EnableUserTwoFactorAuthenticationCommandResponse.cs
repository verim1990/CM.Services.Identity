using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses
{
    public class EnableUserTwoFactorAuthenticationCommandResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}