using CM.Services.Identity.Contract.User.Password.Application.Queries;
using CM.Services.Identity.Contract.User.Password.Application.Queries.Results;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.User.Password.Application.QueryHandlers
{
    public class HasUserPasswordQueryHandler : IRequestHandler<HasUserPasswordQuery, HasUserPasswordQueryResult>
    {
        private readonly IUserPasswordService _userPasswordService;

        public HasUserPasswordQueryHandler(IUserPasswordService userPasswordService)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<HasUserPasswordQueryResult> Handle(HasUserPasswordQuery query, CancellationToken token)
        {
            var hasUserPassword = await _userPasswordService.HasUserPassword(query.User);

            return new HasUserPasswordQueryResult()
            {
                HasUserPassword = hasUserPassword
            };
        }
    }
}