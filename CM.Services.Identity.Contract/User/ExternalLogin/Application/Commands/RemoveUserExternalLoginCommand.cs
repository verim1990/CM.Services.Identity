using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands
{
    public class RemoveUserExternalLoginCommand : IRequest<RemoveUserExternalLoginResponse>
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public ApplicationUser User { get; set; }
    }
}