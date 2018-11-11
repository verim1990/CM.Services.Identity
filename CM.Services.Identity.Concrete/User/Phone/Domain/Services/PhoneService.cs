using CM.Services.Identity.Contract.User.Phone.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Phone.Domain.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PhoneService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SetUserPhone(ApplicationUser user, string newPhone)
        {
            return await _userManager.SetPhoneNumberAsync(user, newPhone);
        }
    }
}