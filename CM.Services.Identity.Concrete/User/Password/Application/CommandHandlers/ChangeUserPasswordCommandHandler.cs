using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ChangeUserPasswordResponse>
    {
        private readonly IUserPasswordService _userPasswordService;

        public ChangeUserPasswordCommandHandler(IUserPasswordService userPasswordService)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<ChangeUserPasswordResponse> Handle(ChangeUserPasswordCommand command, CancellationToken token)
        {
            var identityResult = await _userPasswordService.ChangeUserPassword(command.User, command.OldPassword, command.NewPassword);

            return new ChangeUserPasswordResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}