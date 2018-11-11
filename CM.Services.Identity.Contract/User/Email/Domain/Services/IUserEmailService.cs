using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.Email.Domain.Services
{
    public interface IUserEmailService
    {
        Task<IdentityResult> ConfirmUserEmail(ApplicationUser user, string code);

        Task<IdentityResult> SetUserEmail(ApplicationUser user, string newEmail);

        Task<string> GenerateUserEmailConfirmationToken(ApplicationUser user);
    }
}