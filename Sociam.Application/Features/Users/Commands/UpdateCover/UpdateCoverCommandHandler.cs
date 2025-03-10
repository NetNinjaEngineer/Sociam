using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Commands.UpdateCover;
public sealed class UpdateCoverCommandHandler(IUserService service) : IRequestHandler<UpdateCoverCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCoverCommand request, CancellationToken cancellationToken)
    {
        return await service.UpdateUserCoverAsync(request);
    }
}
