using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.GetUnreadMessagesCount;
public sealed class GetUnreadMessagesCountQueryHandler(IMessageService service) :
    IRequestHandler<GetUnreadMessagesCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(
        GetUnreadMessagesCountQuery request, CancellationToken cancellationToken)
        => await service.GetUnreadMessageCountAsync(request);
}
