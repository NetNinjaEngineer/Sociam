using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Groups.Queries.GetGroupsWithParams
{
    public sealed class GetGroupsWithParamsQuery : IRequest<Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>>
    {
        public GroupParams GroupParams { get; set; } = null!;
    }
}
