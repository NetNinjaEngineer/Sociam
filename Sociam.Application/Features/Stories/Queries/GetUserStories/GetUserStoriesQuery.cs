using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetUserStories;
public sealed class GetUserStoriesQuery : IRequest<Result<UserWithStoriesDto?>>
{
    public string FriendId { get; set; } = null!;
}
