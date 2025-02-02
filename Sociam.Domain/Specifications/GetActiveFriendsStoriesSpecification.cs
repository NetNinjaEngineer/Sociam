using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Specifications;

public sealed class GetActiveFriendsStoriesSpecification : BaseSpecification<Story>
{
    public GetActiveFriendsStoriesSpecification(IEnumerable<string> friendIds, string currentUserId)
        : base(story =>
            friendIds.Contains(story.UserId) &&
            story.ExpiresAt > DateTimeOffset.Now &&
            (story.StoryPrivacy == StoryPrivacy.Public ||
             story.StoryPrivacy == StoryPrivacy.Friends ||
             (story.StoryPrivacy == StoryPrivacy.Custom && story.StoryViewers.Any(storyView => storyView.ViewerId == currentUserId && !storyView.IsViewed))))
    {
        AddIncludes(x => x.User);
        AddIncludes(x => x.StoryViewers);
        DisableTracking();
    }
}