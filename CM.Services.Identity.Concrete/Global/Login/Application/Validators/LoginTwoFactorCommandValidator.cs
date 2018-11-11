using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.Global.Login.Application.Validators
{
    public class LoginTwoFactorCommandValidator : AbstractValidator<LoginTwoFactorCommand>
    {
        public LoginTwoFactorCommandValidator()
        {
            RuleFor(cmd => cmd.AuthenticatorCode)
                .NotEmpty();
        }
    }
}
