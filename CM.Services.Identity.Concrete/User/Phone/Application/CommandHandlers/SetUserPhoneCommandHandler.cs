using CM.Services.Identity.Contract.User.Phone.Application.Commands;
using CM.Services.Identity.Contract.User.Phone.Application.Commands.Responses;
using CM.Services.Identity.Contract.User.Phone.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace CM.Services.Identity.Concrete.User.Phone.Application.CommandHandlers
{
    public class SetUserPhoneCommandHandler : IRequestHandler<SetUserPhoneCommand, SetUserPhoneResponse>
    {
        private readonly IPhoneService _userService;
        private readonly IIdentityService _identityService;

        public SetUserPhoneCommandHandler(
            IPhoneService userService,
            IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        public async Task<SetUserPhoneResponse> Handle(SetUserPhoneCommand command, CancellationToken token)
        {
            var identityResult = await _userService.SetUserPhone(command.User, command.NewPhone);

            return new SetUserPhoneResponse()
            {
                IdentityResult = identityResult
            };
        }
    }
}