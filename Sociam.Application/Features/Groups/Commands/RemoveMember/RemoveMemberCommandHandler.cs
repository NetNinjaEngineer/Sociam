using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.RemoveMember
{
    public sealed class RemoveMemberCommandHandler(IGroupService groupService) : IRequestHandler<RemoveMemberCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(RemoveMemberCommand request,
                                         CancellationToken cancellationToken)
        => await groupService.RemoveMemberFromGroupAsync(request);
    }
}
