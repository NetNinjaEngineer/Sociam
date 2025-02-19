using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Queries.GetReadCount;
public sealed class GetReadCountQueryHandler(INotificationService service)
    : IRequestHandler<GetReadCountQuery, Result<long>>
{
    public async Task<Result<long>> Handle(GetReadCountQuery request, CancellationToken cancellationToken)
        => await service.GetReadNotificationsCountAsync();
}
