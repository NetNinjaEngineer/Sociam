using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Queries.GetUnreadMessages;
public sealed class GetUnreadMessagesQuery : IRequest<Result<IEnumerable<MessageDto>>>
{
    public required string UserId { get; set; }
}
