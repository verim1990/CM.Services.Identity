using CM.Services.Identity.Contract.User.Email.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Email.Application.Validators
{
    public class SetUserEmailCommandValidator : AbstractValidator<SetUserEmailCommand>
    {
        public SetUserEmailCommandValidator()
        {
            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
