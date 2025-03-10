using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.UpdateUserProfile;
public sealed class UpdateUserProfileCommandHandler(IUserService service) : IRequestHandler<UpdateUserProfileCommand, Result<ProfileDto>>
{
    public async Task<Result<ProfileDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateUserProfileAsync(request);
    }
}
