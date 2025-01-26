using FluentValidation;
using Sociam.Application.Helpers;

namespace Sociam.Application.Features.Groups.Commands.CreateNewGroup;

public sealed class CreateNewGroupCommandValidator : AbstractValidator<CreateNewGroupCommand>
{
    public CreateNewGroupCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} has at maximum 100 characters.");


        RuleFor(c => c.GroupPictureUrl)
            .Must((command, file) =>
            {
                if (file is null) return true;
                var fileExtension = Path.GetExtension(file?.FileName)?.ToLower();
                return FileFormats.AllowedImageFormats.Contains(fileExtension!);
            }).WithMessage("Invalid group picture format.");
    }
}
