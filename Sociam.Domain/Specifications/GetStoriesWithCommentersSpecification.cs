using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetStoriesWithCommentersSpecification : BaseSpecification<Story>
{
    public GetStoriesWithCommentersSpecification(string creatorId)
        : base(s => s.UserId == creatorId)
    {
        AddIncludes(s => s.StoryComments);
    }
}