using System.Collections.Generic;

namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses
{
    public class GenerateUserRecoveryCodesCommandResponse
    {
        public List<string> RecoveryCodes { get; set; }
    }
}