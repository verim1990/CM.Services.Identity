using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Authentication.Domain.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            IMediator mediator,
            IPasswordHasher<ApplicationUser> passwordHasher,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUser(ClaimsPrincipal claims)
        {
            return await _userManager.GetUserAsync(claims);
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        
        public async Task<ApplicationUser> GetTwoFactorAuthenticationUser()
        {
            return await _signInManager.GetTwoFactorAuthenticationUserAsync();
        }
    }
}