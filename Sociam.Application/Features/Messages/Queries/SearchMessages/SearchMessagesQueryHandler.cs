using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.SearchMessages;
public sealed class SearchMessagesQueryHandler(
    IMessageService service) : IRequestHandler<SearchMessagesQuery, Result<IEnumerable<MessageDto>>>
{
    public async Task<Result<IEnumerable<MessageDto>>> Handle(
        SearchMessagesQuery request,
        CancellationToken cancellationToken)
        => await service.SearchMessagesAsync(request);
}
