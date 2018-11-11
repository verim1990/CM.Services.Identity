using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands
{
    public class AddUserExternalLoginCommand : IRequest<AddUserExternalLoginResponse>
    {
        public ApplicationUser User { get; set; }
    }
}