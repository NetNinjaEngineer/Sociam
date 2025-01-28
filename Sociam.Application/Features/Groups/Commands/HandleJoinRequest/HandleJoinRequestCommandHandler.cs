using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.HandleJoinRequest
{
    public sealed class HandleJoinRequestCommandHandler(IGroupService groupService)
        : IRequestHandler<HandleJoinRequestCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(
            HandleJoinRequestCommand request, CancellationToken cancellationToken)
            => await groupService.ManageJoinGroupRequestAsync(request);
    }
}
