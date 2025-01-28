using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Queries.GetGroup;

public sealed class GetGroupQueryHandler(IGroupService groupService)
    : IRequestHandler<GetGroupQuery, Result<GroupListDto>>
{
    public async Task<Result<GroupListDto>> Handle(
        GetGroupQuery request, CancellationToken cancellationToken) => await groupService.MeGetGroupAsync(request);
}
