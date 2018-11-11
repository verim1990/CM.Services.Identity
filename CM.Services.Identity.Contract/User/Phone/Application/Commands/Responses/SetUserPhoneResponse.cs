using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.User.Phone.Application.Commands.Responses
{
    public class SetUserPhoneResponse
    {
        public IdentityResult IdentityResult { get; set; }
    }
}