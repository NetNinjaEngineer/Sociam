using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Queries.SearchMessages;
public sealed class SearchMessagesQuery : IRequest<Result<IEnumerable<MessageDto>>>
{
    public required string SearchTerm { get; set; }
    public Guid? ConversationId { get; set; } = null;
}
