using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CM.Services.Identity.Contract.User.ExternalLogin.Presentation.ViewModels
{
    public class UserExternalLoginDetailsViewModel
    {
        public IList<UserLoginInfo> UserExternalLogins { get; set; }

        public IList<UserLoginInfo> OtherExternalLogins { get; set; }

        public bool ShowRemoveButton { get; set; }
    }
}
