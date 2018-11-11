using CM.Services.Identity.Contract.User.Email.Application.Commands;
using CM.Services.Identity.Contract.User.Email.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Email.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Email.Application.CommandHandlers
{
    public class GenerateUserEmailConfirmationTokenCommandHandler :
        IRequestHandler<GenerateUserEmailConfirmationTokenCommand, GenerateUserEmailConfirmationTokenResponse>
    {
        private readonly IUserEmailService _userEmailService;

        public GenerateUserEmailConfirmationTokenCommandHandler(IUserEmailService userEmailService)
        {
            _userEmailService = userEmailService;
        }

        public async Task<GenerateUserEmailConfirmationTokenResponse> Handle(GenerateUserEmailConfirmationTokenCommand request, CancellationToken token)
        {
            var confirmationTtoken = await _userEmailService.GenerateUserEmailConfirmationToken(request.User);

            return new GenerateUserEmailConfirmationTokenResponse()
            {
                Token = confirmationTtoken
            };
        }
    }
}