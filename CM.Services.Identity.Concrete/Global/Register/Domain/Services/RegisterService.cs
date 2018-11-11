using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Contract.Global.Register.Domain.Models;
using CM.Services.Identity.Contract.Global.Register.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Register.Domain.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResult> Register(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                LastName = "asdasd",
                Name = "asdasd",
                PhoneNumber = "asdasd",

            };
            var result = await _userManager.CreateAsync(user, password);

           return new RegisterResult()
           {
               IdentityResult = result,
               User = user
           };
        }
    }
}