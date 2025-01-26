using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Messages.Queries.GetUnreadMessagesCount;
public sealed class GetUnreadMessagesCountQuery : IRequest<Result<int>>
{
    public required string UserId { get; set; }
}
