using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, ChangeUserPasswordResponse>
    {
        private readonly IUserPasswordService _userPasswordService;

        public SetUserPasswordCommandHandler(IUserPasswordService userPasswordService)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<ChangeUserPasswordResponse> Handle(SetUserPasswordCommand command, CancellationToken token)
        {
            var identityResult = await _userPasswordService.SetUserPassword(command.User, command.NewPassword);

            return new ChangeUserPasswordResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}