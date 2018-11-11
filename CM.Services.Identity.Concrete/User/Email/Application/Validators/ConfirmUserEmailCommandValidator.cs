using CM.Services.Identity.Contract.User.Email.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Email.Application.Validators
{
    public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
    {
        public ConfirmUserEmailCommandValidator()
        {
            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
