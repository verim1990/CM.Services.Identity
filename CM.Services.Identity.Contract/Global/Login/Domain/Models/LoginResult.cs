
using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.Global.Login.Domain.Models
{
    public class LoginResult
    {
        public SignInResult SignInResult { get; set; }

        public bool EmailNotConfirmed { get; set; }
    }
}