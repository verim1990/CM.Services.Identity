using CM.Services.Identity.API.Controllers;
using CM.Services.Identity.API.Extentions;
using CM.Services.Identity.Contract.User.Email.Application.Commands;
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
    public class EmailController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public EmailController(
            IMediator mediator,
            IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string userId, string token)
        {
            if (userId == null || token == null)
                return Redirect(Url.HomeLink());

            try
            {
                var user = await _identityService.GetUserById(userId);
                var confirmUserEmailResponse = await _mediator.Send(new ConfirmUserEmailCommand()
                {
                    Token = token,
                    User = user
                });

                if (confirmUserEmailResponse.IdentityResult.Succeeded)
                    return Redirect(Url.LoginLink());

                if (confirmUserEmailResponse.IdentityResult.Errors.Count() > 0)
                    AddErrors(confirmUserEmailResponse.IdentityResult);
            }
            catch (ValidationException ex)
            {
                AddErrors(ex);
            }

            return View("Error");
        }
    }
}