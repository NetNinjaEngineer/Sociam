using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Queries.GetMessagesByDateRange;
public sealed class GetMessagesByDateRangeQuery : IRequest<Result<IEnumerable<MessageDto>>>
{
    public required DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid? ConversationId { get; set; } = null;
}
