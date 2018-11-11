using CM.Services.Identity.Contract.User.Email.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Email.Domain.Services
{
    public class UserEmailService : IUserEmailService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserEmailService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> ConfirmUserEmail(ApplicationUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> SetUserEmail(ApplicationUser user, string newEmail)
        {
            return await _userManager.SetEmailAsync(user, newEmail);
        }

        public async Task<string> GenerateUserEmailConfirmationToken(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}