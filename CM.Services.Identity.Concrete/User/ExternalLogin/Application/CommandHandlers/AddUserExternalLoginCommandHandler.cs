using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class AddUserExternalLoginCommandHandler : IRequestHandler<AddUserExternalLoginCommand, AddUserExternalLoginResponse>
    {
        private readonly IUserExternalLoginService _userExternalLoginService;

        public AddUserExternalLoginCommandHandler(IUserExternalLoginService userExternalLoginService)
        {
            _userExternalLoginService = userExternalLoginService;
        }

        public async Task<AddUserExternalLoginResponse> Handle(AddUserExternalLoginCommand command, CancellationToken token)
        {
            var userExternalLoginInfo = await _userExternalLoginService.GetUserExternalLoginInfo(command.User);

            if (userExternalLoginInfo == null)
            {
                return new AddUserExternalLoginResponse()
                {
                    IdentityResult = IdentityResult.Failed(new IdentityError()
                    {
                        Description = $"Unexpected error occurred loading external login info for user with ID '{command.User.Id}'."
                    })
                };
            }

            var identityResult = await _userExternalLoginService.AddUserExternalLogin(command.User, userExternalLoginInfo);

            return new AddUserExternalLoginResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}