using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.GetMessagesByDateRange;
public sealed class GetMessagesByDateRangeQueryHandler(IMessageService service)
    : IRequestHandler<GetMessagesByDateRangeQuery, Result<IEnumerable<MessageDto>>>
{
    public async Task<Result<IEnumerable<MessageDto>>> Handle(GetMessagesByDateRangeQuery request,
        CancellationToken cancellationToken)
        => await service.GetMessagesByDateRangeAsync(request);
}
