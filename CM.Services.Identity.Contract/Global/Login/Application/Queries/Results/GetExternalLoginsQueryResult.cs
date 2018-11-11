using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CM.Services.Identity.Contract.Global.Login.Application.Queries.Results
{
    public class GetExternalLoginsQueryResult
    {
        public IEnumerable<UserLoginInfo> ExternalLogins { get; set; }
    }
}