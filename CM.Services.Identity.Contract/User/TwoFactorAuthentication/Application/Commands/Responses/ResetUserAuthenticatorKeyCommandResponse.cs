using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses
{
    public class ResetUserAuthenticatorKeyCommandResponse
    {
        public bool Succeeded { get; set; }
    }
}