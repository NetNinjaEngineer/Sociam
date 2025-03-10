using FluentValidation;

namespace Sociam.Application.Features.Users.Commands.UpdateUserProfile;

public sealed class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(c => c.Gender)
            .IsInEnum().WithMessage("Invalid gender specified it must be (Male / Female)");

        RuleFor(c => c.FirstName)
            .NotNull().WithMessage("First name can not be null value.")
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(c => c.LastName)
            .NotNull().WithMessage("Last name can not be null value.")
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

    }
}