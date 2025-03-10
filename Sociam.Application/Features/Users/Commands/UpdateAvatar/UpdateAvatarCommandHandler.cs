using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.UpdateAvatar;
public sealed class UpdateAvatarCommandHandler(IUserService service) : IRequestHandler<UpdateAvatarCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateUserAvatarAsync(request);
    }
}
