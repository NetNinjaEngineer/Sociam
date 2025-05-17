using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Posts.Queries.GetPostReactions;

public sealed class GetPostReactionsQuery(Guid postId, PostReactionsParams @params) : IRequest<Result<PagedResult<PostReactionDto>>>
{
    public Guid PostId { get; } = postId;
    public PostReactionsParams PostReactionsParams { get; } = @params;
}
