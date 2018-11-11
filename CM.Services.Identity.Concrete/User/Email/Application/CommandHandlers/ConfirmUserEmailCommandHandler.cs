using CM.Services.Identity.Contract.User.Email.Application.Commands;
using CM.Services.Identity.Contract.User.Email.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Email.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Email.Application.CommandHandlers
{
    public class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommand, ConfirmUserEmailResponse>
    {
        private readonly IUserEmailService _userEmailService;

        public ConfirmUserEmailCommandHandler(IUserEmailService userEmailService)
        {
            _userEmailService = userEmailService;
        }

        public async Task<ConfirmUserEmailResponse> Handle(ConfirmUserEmailCommand command, CancellationToken token)
        {
            var identityResult = await _userEmailService.ConfirmUserEmail(command.User, command.Token);

            return new ConfirmUserEmailResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}