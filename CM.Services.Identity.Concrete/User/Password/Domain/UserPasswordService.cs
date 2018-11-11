using CM.Services.Identity.Contract.User.Password.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Authentication.Domain.Services
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPasswordService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> HasUserPassword(ApplicationUser user)
        {
            return await _userManager.HasPasswordAsync(user);
        }

        public async Task<string> GenerateUserPasswordResetToken(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> SetUserPassword(ApplicationUser user, string newPassword)
        {
            return await _userManager.AddPasswordAsync(user, newPassword);
        }

        public async Task<IdentityResult> ChangeUserPassword(ApplicationUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ResetUserPassword(ApplicationUser user, string code, string password)
        {
            return await _userManager.ResetPasswordAsync(user, code, password);
        }
    }
}