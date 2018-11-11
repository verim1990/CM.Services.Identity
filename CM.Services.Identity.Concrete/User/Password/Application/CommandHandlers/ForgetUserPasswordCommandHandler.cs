using CM.Services.Identity.Contract.User.Password.Application.Commands;
using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.CommandHandlers
{
    public class ForgetUserPasswordCommandHandler : IRequestHandler<ForgetUserPasswordCommand, ForgetUserPasswordResponse>
    {
        private readonly IUserPasswordService _userPasswordService;

        public ForgetUserPasswordCommandHandler(IUserPasswordService userPasswordService)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<ForgetUserPasswordResponse> Handle(ForgetUserPasswordCommand command, CancellationToken token)
        {
            var resetPasswordtoken = await _userPasswordService.GenerateUserPasswordResetToken(command.User);

            return new ForgetUserPasswordResponse()
            {
                Token = resetPasswordtoken
            };
        }
    }
}