using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Queries.GetUnReadCount;
public sealed class GetUnReadCountQueryHandler(INotificationService service) : IRequestHandler<GetUnReadCountQuery, Result<long>>
{
    public async Task<Result<long>> Handle(GetUnReadCountQuery request, CancellationToken cancellationToken)
        => await service.GetUnReadNotificationsCountAsync();
}
