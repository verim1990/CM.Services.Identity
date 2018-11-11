using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.Password.Domain.Services
{
    public interface IUserPasswordService
    {
        Task<bool> HasUserPassword(ApplicationUser user);

        Task<string> GenerateUserPasswordResetToken(ApplicationUser user);

        Task<IdentityResult> SetUserPassword(ApplicationUser user, string newPassword);

        Task<IdentityResult> ChangeUserPassword(ApplicationUser user, string oldPassword, string newPassword);

        Task<IdentityResult> ResetUserPassword(ApplicationUser user, string code, string password);
    }
}