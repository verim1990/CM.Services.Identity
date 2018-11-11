using CM.Services.Identity.API.Controllers;
using CM.Services.Identity.API.Extentions;
using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Presentation.ViewModels;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Shared.Kernel.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Areas.Public.Controllers
{
    [Area("Public")]
    public class PasswordController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailSender;
        private readonly IIdentityService _identityService;

        public PasswordController(
            IMediator mediator,
            IEmailService emailSender,
            IIdentityService identityService)
        {
            _mediator = mediator;
            _emailSender = emailSender;
            _identityService = identityService;
        }

        #region Forgot password

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forgot()
        {
            return View(new ForgotUserPasswordViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(ForgotUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUserByEmail(model.Email);
                var forgetUserPasswordResponse = await _mediator.Send(new ForgetUserPasswordCommand()
                {
                    User = user
                });

                var callbackUrl = Url.ResetPasswordCallbackLink(
                    user.Id,
                    forgetUserPasswordResponse.Token,
                    Request.Scheme);

                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

                return RedirectToAction(nameof(ForgotConfirmation));
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotConfirmation()
        {
            return View();
        }

        #endregion

        #region Reset password

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Reset(string userId = null, string token = null)
        {
            if (token == null)
                return Redirect(Url.HomeLink());

            var model = new ResetUserPasswordViewModel
            {
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(ResetUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUserByEmail(model.Email);
                var resetUserPasswordResponse = await _mediator.Send(new ResetUserPasswordCommand()
                {
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Token = model.Token,
                    User = user
                });

                if (resetUserPasswordResponse.IdentityResult.Succeeded)
                    return RedirectToAction(nameof(ResetConfirmation));

                if (resetUserPasswordResponse.IdentityResult.Errors.Count() > 0)
                    AddErrors(resetUserPasswordResponse.IdentityResult);
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetConfirmation()
        {
            return View();
        }

        #endregion
    }
}