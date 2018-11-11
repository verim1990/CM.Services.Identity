using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.Global.Register.Domain.Models
{
    public class RegisterResult
    {
        public IdentityResult IdentityResult { get; set; }

        public ApplicationUser User { get; set; }
    }
}