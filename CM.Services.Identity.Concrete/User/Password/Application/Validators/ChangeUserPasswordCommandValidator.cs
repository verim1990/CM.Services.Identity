using CM.Services.Identity.Contract.User.Password.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Password.Application.Validators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(cmd => cmd.NewPassword)
                .NotEmpty();

            RuleFor(cmd => cmd.NewPassword)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(cmd => cmd.ConfirmPassword)
                .NotEmpty()
                .Equal(cmd => cmd.NewPassword)
                .MinimumLength(5);

            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
