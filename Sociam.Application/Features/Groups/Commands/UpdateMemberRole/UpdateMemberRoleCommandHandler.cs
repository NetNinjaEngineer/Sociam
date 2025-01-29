using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.UpdateMemberRole
{
    public sealed class UpdateMemberRoleCommandHandler(IGroupService groupService)
        : IRequestHandler<UpdateMemberRoleCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            UpdateMemberRoleCommand request,
            CancellationToken cancellationToken) => await groupService.UpdateMemberRoleAsync(request);
    }
}
