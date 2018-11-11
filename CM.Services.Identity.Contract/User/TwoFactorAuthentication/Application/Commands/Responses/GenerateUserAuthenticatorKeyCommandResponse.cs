namespace CM.Services.Identity.Contract.User.TwoFactorAuthentication.Application.Commands.Responses
{
    public class GenerateUserAuthenticatorKeyCommandResponse
    {
        public string AuthenticatorKey { get; set; }
    }
}