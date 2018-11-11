using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Queries;
using CM.Services.Identity.Contract.User.Password.Presentation.ViewModels;
using CM.Shared.Kernel.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Controllers.Admin
{
    [Area("Admin")]
    public class PasswordController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public PasswordController(
            IMediator mediator,
            IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        #region Set password

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _identityService.GetUser(User);
            var userHasPasswordResult = await _mediator.Send(new HasUserPasswordQuery()
            {
                User = user
            });

            if (userHasPasswordResult.HasUserPassword)
                return RedirectToAction(nameof(ChangePassword));

            var model = new SetUserPasswordViewModel { StatusMessage = StatusMessage };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUser(User);
                var setPasswordResult = await _mediator.Send(new SetUserPasswordCommand()
                {
                    User = user
                });

                if (setPasswordResult.IdentityResult.Succeeded)
                {
                    StatusMessage = "Your password has been set.";

                    return RedirectToAction(nameof(SetPassword));
                }
                else
                {
                    AddErrors(setPasswordResult.IdentityResult);
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(model);
        }

        #endregion

        #region Change password

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _identityService.GetUser(User);
            var userHasPasswordResult = await _mediator.Send(new HasUserPasswordQuery()
            {
                User = user
            });

            if (!userHasPasswordResult.HasUserPassword)
                return RedirectToAction(nameof(SetPassword));

            var model = new ChangeUserPasswordViewModel { StatusMessage = StatusMessage };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _identityService.GetUser(User);
                var changePasswordResult = await _mediator.Send(new ChangeUserPasswordCommand()
                {
                    OldPassword = model.OldPassword,
                    NewPassword = model.NewPassword,
                    ConfirmPassword = model.ConfirmPassword,
                    User = user
                });

                if (changePasswordResult.IdentityResult.Succeeded)
                {
                    var loginResponse = await _mediator.Send(new LoginCommand()
                    {
                        Username = user.Email,
                        Password = model.NewPassword,
                        RememberMe = true
                    });

                    StatusMessage = "Your password has been changed.";

                    return RedirectToAction(nameof(ChangePassword));
                }
                else
                {
                    AddErrors(changePasswordResult.IdentityResult);
                }
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View(model);
        }

        #endregion
    }
}