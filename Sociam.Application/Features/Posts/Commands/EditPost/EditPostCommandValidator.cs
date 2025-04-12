using FluentValidation;
using Sociam.Application.Helpers;

namespace Sociam.Application.Features.Posts.Commands.EditPost
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator()
        {
            RuleFor(x => x.Media)
                .Must(MediaValidationHelper.BeValidMediaFiles)
                .WithMessage("One or more media files are invalid.");

            RuleFor(x => x.Content)
                .MaximumLength(1000)
                .WithMessage("The content cannot exceed 1000 characters.");
        }
    }
}
