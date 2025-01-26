using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.GetUnreadMessages;
public sealed class GetUnreadMessagesQueryHandler(IMessageService service) : IRequestHandler<GetUnreadMessagesQuery, Result<IEnumerable<MessageDto>>>
{
    public async Task<Result<IEnumerable<MessageDto>>> Handle(
        GetUnreadMessagesQuery request, CancellationToken cancellationToken) => await service.GetUnreadMessagesAsync(request);
}
