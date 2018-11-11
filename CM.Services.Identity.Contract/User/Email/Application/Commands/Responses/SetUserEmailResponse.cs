using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.Email.Application.Commands.Responses
{
    public class SetUserEmailResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}