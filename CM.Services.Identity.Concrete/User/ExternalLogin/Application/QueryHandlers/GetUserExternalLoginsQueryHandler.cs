using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results;
using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.QueryHandlers
{
    public class GetUserExternalLoginsQueryHandler : IRequestHandler<GetUserExternalLoginsQuery, GetUserExternalLoginsQueryResult>
    {
        private readonly IExternalLoginService _externalLoginService;

        private readonly IUserExternalLoginService _userExternalLoginService;

        public GetUserExternalLoginsQueryHandler(
            IExternalLoginService externalLoginService,
            IUserExternalLoginService userExternalLoginService)
        {
            _externalLoginService = externalLoginService;
            _userExternalLoginService = userExternalLoginService;
        }

        public async Task<GetUserExternalLoginsQueryResult> Handle(GetUserExternalLoginsQuery query, CancellationToken token)
        {
            var externalLogins = await _externalLoginService.GetExternalLogins();
            var userExternalLogins = await _userExternalLoginService.GetUserExternalLogins(query.User);
            var otherExternalLogins = externalLogins
                .Where(el => userExternalLogins
                    .All(uel => el.LoginProvider!= uel.LoginProvider))
                .ToList();

            return new GetUserExternalLoginsQueryResult()
            {
                UserExternalLogins = userExternalLogins,
                OtherExternalLogins = otherExternalLogins
            };
        }
    }
}