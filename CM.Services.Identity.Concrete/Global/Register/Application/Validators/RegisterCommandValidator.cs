using CM.Services.Identity.Contract.Global.Register.Application.Commands;
using FluentValidation;

namespace CM.Services.Identity.Concrete.Global.Register.Application.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(cmd => cmd.Username)
                .NotEmpty()
                .MinimumLength(5);
                //.Must(userService.IsUsernameUnique);

            RuleFor(cmd => cmd.Email)
                .NotEmpty()
                .EmailAddress();
                //.Must(userService.IsEmailUnique);

            RuleFor(cmd => cmd.Password)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(cmd => cmd.ConfirmPassword)
                .NotEmpty()
                .Equal(cmd => cmd.Password)
                .MinimumLength(5);
        }
    }
}
