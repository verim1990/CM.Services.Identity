using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries.Results;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.QueryHandlers
{
    public class GetUserRecoveryCodesCountQueryHandler : IRequestHandler<GetUserRecoveryCodesCountQuery, GetUserRecoveryCodesCountQueryResult>
    {
        private readonly ITwoFactorAuthenticationService _twoFactorAuthenticationService;

        public GetUserRecoveryCodesCountQueryHandler(ITwoFactorAuthenticationService twoFactorAuthenticationService)
        {
            _twoFactorAuthenticationService = twoFactorAuthenticationService;
        }

        public async Task<GetUserRecoveryCodesCountQueryResult> Handle(GetUserRecoveryCodesCountQuery query, CancellationToken token)
        {
            var count = await _twoFactorAuthenticationService.GetUserRecoveryCodesCount(query.User);

            return new GetUserRecoveryCodesCountQueryResult()
            {
                Count = count 
            };
        }
    }
}