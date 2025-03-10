using FluentValidation;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountPassword;

public sealed class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
{
    public ChangeAccountPasswordCommandValidator()
    {
        RuleFor(c => c.OldPassword)
            .NotEmpty().WithMessage("Old password is required.")
            .NotNull().WithMessage("Old password can not be null.");

        RuleFor(c => c.NewPassword)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password is required")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")
            .WithMessage("Password must be at least 8 characters long, include at least one lowercase letter, one uppercase letter, one digit, and one special character (e.g., !@#$%^&*).")
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords do not match.");
    }
}