using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.Global.Authentication.Domain.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
        }
    }
}