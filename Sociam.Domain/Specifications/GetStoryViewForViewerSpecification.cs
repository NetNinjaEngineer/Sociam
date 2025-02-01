using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetStoryViewForViewerSpecification(string viewerId) : BaseSpecification<StoryView>(
    sv => !sv.IsViewed && sv.ViewerId == viewerId)
{
}