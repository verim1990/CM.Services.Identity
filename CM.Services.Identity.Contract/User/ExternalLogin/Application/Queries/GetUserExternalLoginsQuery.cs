using CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries
{
    public class GetUserExternalLoginsQuery : IRequest<GetUserExternalLoginsQueryResult>
    {
        public ApplicationUser User { get; set; }
    }
}