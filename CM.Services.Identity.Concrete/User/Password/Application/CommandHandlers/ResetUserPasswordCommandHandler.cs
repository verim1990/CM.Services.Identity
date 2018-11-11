using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, ResetUserPasswordResponse>
    {
        private readonly IUserPasswordService _userPasswordService;

        public ResetUserPasswordCommandHandler(IUserPasswordService userPasswordService)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<ResetUserPasswordResponse> Handle(ResetUserPasswordCommand command, CancellationToken token)
        {
            var identityResult = await _userPasswordService.ResetUserPassword(command.User, command.Token, command.Password);

            return new ResetUserPasswordResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}