using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class GetActiveStorySpecification : BaseSpecification<Story>
{
    public GetActiveStorySpecification(Guid storyId, string createdById)
        : base(story => story.UserId == createdById && story.Id == storyId && story.ExpiresAt > DateTimeOffset.Now)
        => AddIncludes(story => story.User);

    public GetActiveStorySpecification(Guid storyId)
        : base(story => story.Id == storyId && story.ExpiresAt > DateTimeOffset.Now) { }
}
