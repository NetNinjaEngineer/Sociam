using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Groups.Queries.GetGroupsWithParams
{
    public sealed class GetGroupsWithParamsQueryHandler(IGroupService groupService)
        : IRequestHandler<GetGroupsWithParamsQuery, Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>>
    {
        public async Task<Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>> Handle(
            GetGroupsWithParamsQuery request, CancellationToken cancellationToken)
        {
            return await groupService.GetAllGroupsWithParamsAsync(request);
        }
    }
}
