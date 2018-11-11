using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.Global.Authentication.Domain.Services
{
    public interface IIdentityService
    {
        Task<ApplicationUser> GetUser(ClaimsPrincipal claims);

        Task<ApplicationUser> GetUserById(string id);

        Task<ApplicationUser> GetUserByEmail(string email);

        Task<ApplicationUser> GetTwoFactorAuthenticationUser();
    }
}