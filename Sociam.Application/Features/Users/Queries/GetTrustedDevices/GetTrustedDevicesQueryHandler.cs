using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Queries.GetTrustedDevices;
public sealed class GetTrustedDevicesQueryHandler(IUserService service) :
    IRequestHandler<GetTrustedDevicesQuery, Result<IReadOnlyList<TrustedDeviceDto>>>
{
    public async Task<Result<IReadOnlyList<TrustedDeviceDto>>> Handle(
        GetTrustedDevicesQuery request, CancellationToken cancellationToken)
        => await service.GetUserTrustedDevicesAsync();
}
