using CM.Services.Identity.Contract.User.Password.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Password.Application.Validators
{
    public class ForgetPasswordCommandValidator : AbstractValidator<ForgetUserPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
