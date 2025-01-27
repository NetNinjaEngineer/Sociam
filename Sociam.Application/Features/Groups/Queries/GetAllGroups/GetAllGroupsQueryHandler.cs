using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Queries.GetAllGroups;

public sealed class GetAllGroupsQueryHandler(IGroupService groupService) : IRequestHandler<GetAllGroupsQuery, Result<IReadOnlyList<GroupListDto>>>
{
    public async Task<Result<IReadOnlyList<GroupListDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        => await groupService.GetAllGroupsAsync();
}
