using FluentValidation;

namespace Sociam.Application.Features.Stories.Queries.HasUnseenStories;

public sealed class HasUnseenStoriesQueryValidator : AbstractValidator<HasUnseenStoriesQuery>
{
    public HasUnseenStoriesQueryValidator()
    {
        RuleFor(q => q.FriendId)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}