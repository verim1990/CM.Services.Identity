using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results;
using MediatR;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries
{
    public class GetExternalLoginsQuery : IRequest<GetExternalLoginsQueryResult>
    {
    }
}