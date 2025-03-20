using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;

namespace Sociam.Application.Features.Users.Queries.GetTrustedDevices;
public sealed class GetTrustedDevicesQuery : IRequest<Result<IReadOnlyList<TrustedDeviceDto>>>
{
}
