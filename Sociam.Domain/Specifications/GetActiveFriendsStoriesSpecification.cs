using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Specifications;

public sealed class GetActiveFriendsStoriesSpecification : BaseSpecification<Story>
{
    public GetActiveFriendsStoriesSpecification(List<ApplicationUser> friends)
        : base(x => x.ExpiresAt > DateTimeOffset.Now && friends.Select(f => f.Id).Contains(x.UserId))
    {
        AddIncludes(x => x.User);
    }
}