using CM.Services.Identity.Contract.User.Email.Application.Commands;
using CM.Services.Identity.Contract.User.Email.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Email.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Email.Application.CommandHandlers
{
    public class SetUserEmailCommandHandler : IRequestHandler<SetUserEmailCommand, SetUserEmailResponse>
    {
        private readonly IUserEmailService _userEmailService;

        public SetUserEmailCommandHandler(IUserEmailService userEmailService)
        {
            _userEmailService = userEmailService;
        }

        public async Task<SetUserEmailResponse> Handle(SetUserEmailCommand command, CancellationToken token)
        {
            var identityResult = await _userEmailService.SetUserEmail(command.User, command.NewEmail);

            return new SetUserEmailResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}