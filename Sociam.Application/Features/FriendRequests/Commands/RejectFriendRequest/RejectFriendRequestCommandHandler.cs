using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Commands.RejectFriendRequest;
public sealed class RejectFriendRequestCommandHandler(IFriendshipService service)
    : IRequestHandler<RejectFriendRequestCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        RejectFriendRequestCommand request, CancellationToken cancellationToken)
    {
        return await service.RejectFriendRequestAsync(request);
    }
}
