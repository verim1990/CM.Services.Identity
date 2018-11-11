using CM.Services.Identity.Contract.User.Password.Application.Queries.Results;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.Password.Application.Queries
{
    public class HasUserPasswordQuery : IRequest<HasUserPasswordQueryResult>
    {
        public ApplicationUser User { get; set; }
    }
}