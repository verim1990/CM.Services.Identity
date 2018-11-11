using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries;
using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results;
using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.QueryHandlers
{
    public class GetExternalLoginsQueryHandler : IRequestHandler<GetExternalLoginsQuery, GetExternalLoginsQueryResult>
    {
        private readonly IExternalLoginService _externalLoginService;

        public GetExternalLoginsQueryHandler(IExternalLoginService externalLoginService)
        {
            _externalLoginService = externalLoginService;
        }

        public async Task<GetExternalLoginsQueryResult> Handle(GetExternalLoginsQuery query, CancellationToken token)
        {
            var externalLogins = await _externalLoginService.GetExternalLogins();

            return new GetExternalLoginsQueryResult()
            {
                ExternalLogins = externalLogins
            };
        }
    }
}