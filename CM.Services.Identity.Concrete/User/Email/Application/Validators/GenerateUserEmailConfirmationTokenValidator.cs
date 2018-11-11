using CM.Services.Identity.Contract.User.Email.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.User.Email.Application.Validators
{
    public class GenerateUserEmailConfirmationTokenValidator : AbstractValidator<GenerateUserEmailConfirmationTokenCommand>
    {
        public GenerateUserEmailConfirmationTokenValidator()
        {
            RuleFor(cmd => cmd.User)
                .NotNull();
        }
    }
}
