using FluentValidation;
using Sociam.Application.Helpers;

namespace Sociam.Application.Features.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Media)
                .Must(MediaValidationHelper.BeValidMediaFiles)
                .WithMessage("One or more media files are invalid.");

            RuleFor(x => x.Text)
                .MaximumLength(1000)
                .WithMessage("The text cannot exceed 1000 characters.");
        }
    }
}
