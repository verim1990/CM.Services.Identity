using CM.Services.Identity.API.Models.ManageViewModels;
using CM.Services.Identity.Contract.User.Email.Application.Commands;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries;
using CM.Services.Identity.Contract.User.ExternalLogin.Presentation.ViewModels;
using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Queries;
using CM.Services.Identity.Contract.User.Password.Presentation.ViewModels;
using CM.Services.Identity.Contract.User.Phone.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Presentation.ViewModels;
using CM.Services.Identity.Contract.User.User.Presentation.ViewModels;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Presentation.ViewModels;
using CM.Shared.Kernel.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class ManageController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailService emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          IMediator mediator,
          IIdentityService identityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _mediator = mediator;
            _identityService = identityService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        #region User details

        [HttpGet]
        public async Task<IActionResult> UserDetails()
        {
            var user = await _identityService.GetUser(User);
            var model = new UserDetailsViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDetails(UserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUser(User);
                var setUserEmailResult = await _mediator.Send(new SetUserEmailCommand()
                {
                    NewEmail = model.Email,
                    User = user
                });

                var setUserPhoneResult = await _mediator.Send(new SetUserPhoneCommand()
                {
                    NewPhone = model.PhoneNumber,
                    User = user
                });

                if (!setUserEmailResult.IdentityResult.Succeeded)
                    AddErrors(setUserEmailResult.IdentityResult);

                if (!setUserPhoneResult.IdentityResult.Succeeded)
                    AddErrors(setUserPhoneResult.IdentityResult);

                if (setUserEmailResult.IdentityResult.Succeeded && setUserPhoneResult.IdentityResult.Succeeded)
                {
                    StatusMessage = "Your profile has been updated";

                    return RedirectToAction(nameof(UserDetails));
                }
            }
            catch
            {
                ModelState.AddModelError("", "Expcetion occured");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(UserDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUser(User);
                var generateUserEmailConfirmationTokenResponse = await _mediator.Send(new GenerateUserEmailConfirmationTokenCommand()
                {
                    User = user
                });

                if (!string.IsNullOrEmpty(generateUserEmailConfirmationTokenResponse.Token))
                {
                    //var callbackUrl = Url.Action(
                    //    action: nameof(AccountController.ConfirmEmail),
                    //    controller: "Account",
                    //    values: new
                    //    {
                    //        user = user.Id.ToString(),
                    //        token = generateUserEmailConfirmationTokenResponse.Token
                    //    },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    //    $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                    StatusMessage = "Verification email sent. Please check your email.";

                    return RedirectToAction(nameof(UserDetails));
                }
                else
                {
                    ModelState.AddModelError("", "Failed");
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(model);
        }

        #endregion

        #region Two factor authorization

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication()
        {
            var user = await _identityService.GetUser(User);

            if (!user.TwoFactorEnabled)
                return View(new TwoFactorAuthenticationViewModel()
                {
                    Is2faEnabled = false
                });

            var getAuthenticatorKeyQueryResult = await _mediator.Send(new GetUserAuthenticatorKeyQuery()
            {
                User = user
            });
            var getRecoveryCodesCountQueryResult = await _mediator.Send(new GetUserRecoveryCodesCountQuery()
            {
                User = user
            });

            var model = new TwoFactorAuthenticationViewModel
            {
                Is2faEnabled = true,
                HasAuthenticator = getAuthenticatorKeyQueryResult.AuthenticatorKey != null,
                RecoveryCodesLeft = getRecoveryCodesCountQueryResult.Count
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Enable2fa()
        {
            var user = await _identityService.GetUser(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured enabling 2FA for user with ID '{user.Id}'.");
            }

            return View();
        }

        [HttpPost]
        [ActionName("Enable2fa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable2faPost()
        {
            try
            {
                var user = await _identityService.GetUser(User);
                var enableTwoFactorAuthenticationCommandResult = await _mediator.Send(new EnableUserTwoFactorAuthenticationCommand()
                {
                    User = user
                });

                if (enableTwoFactorAuthenticationCommandResult.IdentityResult.Succeeded)
                    return RedirectToAction(nameof(GenerateRecoveryCodes));
                else
                    AddErrors(enableTwoFactorAuthenticationCommandResult.IdentityResult);
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Disable2fa()
        {
            var user = await _identityService.GetUser(User);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!user.TwoFactorEnabled)
            {
                throw new ApplicationException($"Unexpected error occured disabling 2FA for user with ID '{user.Id}'.");
            }

            return View();
        }

        [HttpPost]
        [ActionName("Disable2fa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable2faPost()
        {
            try
            {
                var user = await _identityService.GetUser(User);
                var disableTwoFactorAuthenticationCommandResult = await _mediator.Send(new DisableUserTwoFactorAuthenticationCommand()
                {
                    User = user
                });

                if (disableTwoFactorAuthenticationCommandResult.IdentityResult.Succeeded)
                    return RedirectToAction(nameof(TwoFactorAuthentication));
                else
                    AddErrors(disableTwoFactorAuthenticationCommandResult.IdentityResult);
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        #endregion

        #region Authenticator

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _identityService.GetUser(User);
            var generateAuthenticatorKeyCommandResponse = await _mediator.Send(new GenerateUserAuthenticatorKeyCommand()
            {
                User = user
            });

            var model = new EnableUserAuthenticatorViewModel
            {
                SharedKey = FormatKey(generateAuthenticatorKeyCommandResponse.AuthenticatorKey),
                AuthenticatorUri = GenerateQrCodeUri(user.Email, generateAuthenticatorKeyCommandResponse.AuthenticatorKey)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableUserAuthenticatorViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUser(User);
                var enableAuthenticatorCommandResult = await _mediator.Send(new EnableUserAuthenticatorCommand()
                {
                    VerificationCode = model.Code.Replace(" ", string.Empty).Replace("-", string.Empty),
                    User = user
                });

                if (enableAuthenticatorCommandResult.Succeeded)
                {
                    return RedirectToAction(nameof(GenerateRecoveryCodes));
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetAuthenticator()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ResetAuthenticator")]
        [ValidateAntiForgeryToken]
        public IActionResult ResetAuthenticatorPost()
        {
            return RedirectToAction(nameof(EnableAuthenticator));
        }

        #endregion

        #region Recovery codes

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            try
            {
                var user = await _identityService.GetUser(User);
                var generateRecoveryCodesCommandResponse = await _mediator.Send(new GenerateUserRecoveryCodesCommand()
                {
                    Count = 10,
                    User = user
                });

                return View(new GenerateUserRecoveryCodesViewModel
                {
                    RecoveryCodes = generateRecoveryCodesCommandResponse.RecoveryCodes.ToArray()
                });
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(nameof(TwoFactorAuthentication));
        }

        #endregion

        #region External login

        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            try
            {
                var user = await _identityService.GetUser(User);
                var hasUserPasswordResult = await _mediator.Send(new HasUserPasswordQuery()
                {
                    User = user
                });
                var getUserLoginsResult = await _mediator.Send(new GetUserExternalLoginsQuery()
                {
                    User = user
                });

                return View(new UserExternalLoginDetailsViewModel()
                {
                    UserExternalLogins = getUserLoginsResult.UserExternalLogins.ToList(),
                    OtherExternalLogins = getUserLoginsResult.OtherExternalLogins.ToList(),
                    ShowRemoveButton = hasUserPasswordResult.HasUserPassword || getUserLoginsResult.UserExternalLogins.Count() > 1
                });
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            StatusMessage = "Operation failed";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));

            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            var user = await _identityService.GetUser(User);

            try
            {
                var addLoginResponse = await _mediator.Send(new AddUserExternalLoginCommand()
                {
                    User = user
                });

                if (addLoginResponse.IdentityResult.Succeeded)
                {
                    // Clear the existing external cookie to ensure a clean login process
                    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                    StatusMessage = "The external login was added.";

                    return RedirectToAction(nameof(ExternalLogins));
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveUserExternalLoginViewModel model)
        {
            try
            {
                var user = await _identityService.GetUser(User);
                var removeLoginResponse = await _mediator.Send(new RemoveUserExternalLoginCommand()
                {
                    LoginProvider = model.LoginProvider,
                    ProviderKey = model.ProviderKey,
                    User = user
                });

                if (removeLoginResponse.IdentityResult.Succeeded)
                {
                    StatusMessage = "The external login was removed.";

                    return RedirectToAction(nameof(ExternalLogins));
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            StatusMessage = "Operation Failed.";

            return View(model);
        }

        #endregion

        #region Helpers

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            return string.Format(
                "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
                _urlEncoder.Encode("WebApplication12"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        #endregion
    }
}
