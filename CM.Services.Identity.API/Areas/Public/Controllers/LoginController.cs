using CM.Services.Identity.API.Controllers;
using CM.Services.Identity.API.Extentions;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Presentation.ViewModels;
using CM.Shared.Kernel.Application.Exceptions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Areas.Public.Controllers
{
    [Area("Public")]
    public class LoginController : BaseController
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IMediator mediator,
            IIdentityService identityService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _mediator = mediator;
            _identityService = identityService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Login 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context?.IdP != null)
                return LoginExternal(context.IdP, returnUrl);

            var vm = await BuildLoginViewModelAsync(returnUrl);

            ViewData["ReturnUrl"] = returnUrl;

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try {
                var loginResponse = await _mediator.Send(new LoginCommand()
                {
                    Username = model.Username,
                    Password = model.Password,
                    RememberMe = model.RememberMe
                });

                if (loginResponse.LoginResult.SignInResult.Succeeded)
                {
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return Redirect(Url.HomeLink());
                }
                else if (loginResponse.LoginResult.SignInResult.IsLockedOut)
                    return RedirectToAction(nameof(Lockout));
                else if (loginResponse.LoginResult.SignInResult.RequiresTwoFactor)
                    return RedirectToAction(nameof(LoginWith2fa), new { model.ReturnUrl, model.RememberMe });
                else if (loginResponse.LoginResult.EmailNotConfirmed)
                    ModelState.AddModelError("", "Email is not confirmed.");
                else
                    ModelState.AddModelError("", "Invalid username or password.");
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);

            vm.Username = model.Username;
            vm.RememberMe = model.RememberMe;

            ViewData["ReturnUrl"] = model.ReturnUrl;

            return View(vm);
        }

        async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            var getExternalLoginProvidersQueryResult = await _mediator.Send(new GetExternalLoginsQuery());

            var vm = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalProviders = getExternalLoginProvidersQueryResult
                    .ExternalLogins.ToList()
            };

            if (context != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);

                if (client?.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    vm.ExternalProviders = vm.ExternalProviders
                        .Where(provider => client.IdentityProviderRestrictions
                            .Contains(provider.LoginProvider))
                        .ToList();

                vm.Username = context.LoginHint;
                vm.EnableLocalLogin = client?.EnableLocalLogin ?? true;
            }

            return vm;
        }

        #endregion

        #region Login with 2fa

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            var user = await _identityService.GetTwoFactorAuthenticationUser();

            if (user == null)
                return RedirectToAction(nameof(Login));

            var model = new LoginWith2faViewModel {
                RememberMe = rememberMe,
                ReturnUrl = returnUrl
            };

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var loginResponse = await _mediator.Send(new LoginTwoFactorCommand()
                {
                    AuthenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty),
                    RememberMe = model.RememberMe,
                    RememberMachine = model.RememberMachine
                });

                if (loginResponse.LoginResult.SignInResult.Succeeded)
                {
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return Redirect(Url.HomeLink());
                }
                else if (loginResponse.LoginResult.SignInResult.IsLockedOut)
                    return RedirectToAction(nameof(Lockout));
                else
                    ModelState.AddModelError("", "Invalid authenticator code.");
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            ViewData["ReturnUrl"] = model.ReturnUrl;

            return View(model);
        }

        #endregion

        #region Login with recovery code

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            var user = await _identityService.GetTwoFactorAuthenticationUser();

            if (user == null)
                return RedirectToAction(nameof(Login));

            var model = new LoginWithRecoveryCodeViewModel()
            {
                ReturnUrl = returnUrl
            };

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var loginResponse = await _mediator.Send(new LoginRecoveryCommand()
                {
                    RecoveryCode = model.RecoveryCode.Replace(" ", string.Empty),
                });

                if (loginResponse.LoginResult.SignInResult.Succeeded)
                {
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return Redirect(Url.HomeLink());
                }
                else if (loginResponse.LoginResult.SignInResult.IsLockedOut)
                    return RedirectToAction(nameof(Lockout));
                else
                    ModelState.AddModelError("", "Invalid recovery code entered.");
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            ViewData["ReturnUrl"] = model.ReturnUrl;

            return View(model);
        }

        #endregion

        #region Login external

        [HttpPost]
        [AllowAnonymous]
        public IActionResult LoginExternal(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, new AuthenticationProperties
            {
                Items = { { "scheme", provider } },
                RedirectUri = "/public/login/loginexternalcallback?returnUrl=" + 
                    (string.IsNullOrEmpty(returnUrl) ? "" : UrlEncoder.Default.Encode(returnUrl)),
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginExternalCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View(nameof(Login));
            }

            try
            {
                var loginExternalResponse = await _mediator.Send(new LoginExternalCommand());

                if (loginExternalResponse.ExternalLoginInfo == null)
                {
                    return RedirectToAction(nameof(Login));
                }

                if (loginExternalResponse.LoginResult.SignInResult.Succeeded)
                    return Redirect(returnUrl);
                else if (loginExternalResponse.LoginResult.SignInResult.IsLockedOut)
                    return RedirectToAction(nameof(Lockout));
                else if (loginExternalResponse.LoginResult.SignInResult.RequiresTwoFactor)
                    return RedirectToAction(nameof(LoginWith2fa), new { ReturnUrl = returnUrl });
                else
                {
                    // If the user does not have an account, then ask the user to create an account.
                    ViewData["ReturnUrl"] = returnUrl;
                    ViewData["LoginProvider"] = loginExternalResponse.ExternalLoginInfo.LoginProvider;

                    var email = loginExternalResponse.ExternalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

                    return View("ExternalLoginConfirmation", new LoginConfirmationViewModel { Email = email });
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(nameof(Login));
        }

        #endregion

        #region Login confirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginConfirmation(LoginConfirmationViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                View(model);

            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return View("ExternalLoginFailure");
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Redirect(returnUrl);
                }
            }

            AddErrors(result);

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        #endregion

        #region Lockout

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        #endregion
    }
}