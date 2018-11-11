using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.User.Phone.Domain.Services
{
    public interface IPhoneService
    {
        Task<IdentityResult> SetUserPhone(ApplicationUser user, string newPhone);
    }
}