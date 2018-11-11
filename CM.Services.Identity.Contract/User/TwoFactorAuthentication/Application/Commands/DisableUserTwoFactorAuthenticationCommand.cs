using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands
{
    public class DisableUserTwoFactorAuthenticationCommand : IRequest<DisableUserTwoFactorAuthenticationCommandResponse>
    {
        public ApplicationUser User { get; set; }
    }
}