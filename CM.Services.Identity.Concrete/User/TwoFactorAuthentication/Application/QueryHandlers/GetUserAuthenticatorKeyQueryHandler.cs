using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries.Results;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.QueryHandlers
{
    public class GetUserAuthenticatorKeyQueryHandler : IRequestHandler<GetUserAuthenticatorKeyQuery, GetUserAuthenticatorKeyQueryResult>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public GetUserAuthenticatorKeyQueryHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<GetUserAuthenticatorKeyQueryResult> Handle(GetUserAuthenticatorKeyQuery query, CancellationToken token)
        {
            var authenticatorKey = await _twoFactorAuthenticationService.GetUserAuthenticatorKey(query.User);

            return new GetUserAuthenticatorKeyQueryResult()
            {
                AuthenticatorKey = authenticatorKey
            };
        }
    }
}