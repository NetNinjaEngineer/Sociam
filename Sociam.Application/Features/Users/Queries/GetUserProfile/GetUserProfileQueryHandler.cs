using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Queries.GetUserProfile;
public sealed class GetUserProfileQueryHandler(IUserService service) : IRequestHandler<GetUserProfileQuery, Result<ProfileDto?>>
{
    public async Task<Result<ProfileDto?>> Handle(
        GetUserProfileQuery request, CancellationToken cancellationToken) => await service.GetUserProfileAsync();
}
