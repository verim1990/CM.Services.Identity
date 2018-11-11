using CM.Services.Identity.Contract.Global.Login.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses
{
    public class LoginExternalResponse
    {
        public ExternalLoginInfo ExternalLoginInfo { get; set; }

        public LoginResult LoginResult { get; set; }
    }
}