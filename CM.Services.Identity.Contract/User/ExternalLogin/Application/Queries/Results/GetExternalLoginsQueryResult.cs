using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results
{
    public class GetExternalLoginsQueryResult
    {
        public IEnumerable<UserLoginInfo> ExternalLogins { get; set; }
    }
}