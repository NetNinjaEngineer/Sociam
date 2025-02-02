using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Stories.Queries.HasUnseenStories;
public sealed class HasUnseenStoriesQuery : IRequest<Result<bool>>
{
    public string FriendId { get; set; } = null!;
}