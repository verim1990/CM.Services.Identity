using CM.Services.Identity.API.Controllers;
using CM.Services.Identity.API.Extentions;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Register.Application.Commands;
using CM.Services.Identity.Contract.Global.Register.Presentation.ViewModels;
using CM.Shared.Kernel.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Areas.Public.Controllers
{
    [Area("Public")]
    public class RegisterController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailSender;

        public RegisterController(
            IMediator mediator,
            IEmailService emailSender)
        {
            _mediator = mediator;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var registerResponse = await _mediator.Send(new RegisterCommand()
                {
                    Username = model.Email,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                });

                if (registerResponse.RegisterResult.IdentityResult.Succeeded)
                {
                    var callbackUrl = Url.EmailConfirmationLink(
                        registerResponse.RegisterResult.User.Id,
                        registerResponse.Token,
                        Request.Scheme);

                    //await _emailSender.SendEmailAsync(registerResponse.Email, "Confirm your email",
                    //    $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                    if (returnUrl != null && HttpContext.User.Identity.IsAuthenticated)
                        return Redirect(returnUrl);
                    else 
                        return Redirect(Url.LoginLink());
                }

                if (registerResponse.RegisterResult.IdentityResult.Errors.Count() > 0)
                    AddErrors(registerResponse.RegisterResult.IdentityResult);
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }
    }
}