using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.Global.Login.Application.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(cmd => cmd.Username)
                .NotEmpty();

            RuleFor(cmd => cmd.Password)
                .NotEmpty();
        }
    }
}
