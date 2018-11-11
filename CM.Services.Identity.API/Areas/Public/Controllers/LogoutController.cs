using CM.Services.Identity.API.Controllers;
using CM.Services.Identity.Contract.Global.Login.Presentation.ViewModels;
using CM.Shared.Kernel.Application.Exceptions;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Areas.Public.Controllers
{
    [Area("Public")]
    public class LogoutController : BaseController
    {
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        #region Logout

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var model = new LogoutViewModel
            {
                LogoutId = logoutId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            var idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
            {
                if (model.LogoutId == null)
                {
                    // if there's no current logout context, we need to create one
                    // this captures necessary info from the current logged in user
                    // before we signout and redirect away to the external IdP for signout
                    model.LogoutId = await _interaction.CreateLogoutContextAsync();
                }

                string url = "/Account/Logout?logoutId=" + model.LogoutId;

                try
                {
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.SignOutAsync(idp, new AuthenticationProperties
                    {
                        RedirectUri = url
                    });
                }
                catch (ValidationException ex)
                {
                    AddErrors(ex);
                }
                catch
                {

                }
            }

            // delete authentication cookie
            await HttpContext.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

            return Redirect(logout?.PostLogoutRedirectUri);
        }

        #endregion

        #region Device logout

        [HttpGet]
        public async Task<IActionResult> DeviceLogOut(string redirectUrl)
        {
            // delete authentication cookie
            await HttpContext.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            return Redirect(redirectUrl);
        }

        #endregion
    }
}