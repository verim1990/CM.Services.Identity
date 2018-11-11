using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands
{
    public class EnableUserAuthenticatorCommand : IRequest<EnableUserAuthenticatorCommandResponse>
    {
        public string VerificationCode { get; set; }

        public ApplicationUser User { get; set; }
    }
}