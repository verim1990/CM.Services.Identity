using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries.Results;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Queries
{
    public class GetUserRecoveryCodesCountQuery : IRequest<GetUserRecoveryCodesCountQueryResult>
    {
        public ApplicationUser User { get; set; }
    }
}