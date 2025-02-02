using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace Sociam.Domain.Specifications;

public sealed class GetActiveFriendsStoriesSpecification : BaseSpecification<Story>
{
    public GetActiveFriendsStoriesSpecification(List<ApplicationUser> friends)
        : base(BuildCriteria(friends))
    {
        AddIncludes(x => x.User);
        DisableTracking();
    }

    private static Expression<Func<Story, bool>> BuildCriteria(List<ApplicationUser> friends)
    {
        var friendIds = friends.Select(f => f.Id).ToList();
        return x => x.ExpiresAt > DateTimeOffset.UtcNow && friendIds.Contains(x.UserId);
    }
}