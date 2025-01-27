using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Queries.GetGroupById;

public sealed class GetGroupByIdQueryHandler(
    IGroupService groupService) : IRequestHandler<GetGroupByIdQuery, Result<GroupListDto>>
{
    public async Task<Result<GroupListDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        => await groupService.GetGroupByIdAsync(request.GroupId);
}
