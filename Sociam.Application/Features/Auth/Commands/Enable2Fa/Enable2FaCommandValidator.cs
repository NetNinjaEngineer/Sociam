using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.Enable2Fa;

public sealed class Enable2FaCommandValidator : AbstractValidator<Enable2FaCommand>
{
    public Enable2FaCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}
