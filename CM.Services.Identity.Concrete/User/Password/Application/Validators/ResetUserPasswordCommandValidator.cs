using CM.Services.Identity.Contract.User.Password.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Password.Application.Validators
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetUserPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(cmd => cmd.Password)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(cmd => cmd.ConfirmPassword)
                .NotEmpty()
                .Equal(cmd => cmd.Password)
                .MinimumLength(5);

            RuleFor(cmd => cmd.Token)
                .NotEmpty();

            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
