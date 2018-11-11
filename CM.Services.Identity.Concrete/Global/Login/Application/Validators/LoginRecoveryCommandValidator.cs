using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.Global.Login.Application.Validators
{
    public class LoginRecoveryCommandValidator : AbstractValidator<LoginRecoveryCommand>
    {
        public LoginRecoveryCommandValidator()
        {
            RuleFor(cmd => cmd.RecoveryCode)
                .NotEmpty();
        }
    }
}
