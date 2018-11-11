using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class RemoveUserExternalLoginCommandHandler : IRequestHandler<RemoveUserExternalLoginCommand, RemoveUserExternalLoginResponse>
    {
        private readonly IUserExternalLoginService _userExternalLoginService;

        public RemoveUserExternalLoginCommandHandler(IUserExternalLoginService userExternalLoginService)
        {
            _userExternalLoginService = userExternalLoginService;
        }

        public async Task<RemoveUserExternalLoginResponse> Handle(RemoveUserExternalLoginCommand command, CancellationToken token)
        {
            var identityResult = await _userExternalLoginService.RemoveUserExternalLogin(command.User, command.LoginProvider, command.ProviderKey);

            return new RemoveUserExternalLoginResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}