using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Application.Queries.Results
{
    public class GetUserExternalLoginsQueryResult
    {
        public IEnumerable<UserLoginInfo> UserExternalLogins { get; set; }

        public IEnumerable<UserLoginInfo> OtherExternalLogins { get; set; }
    }
}