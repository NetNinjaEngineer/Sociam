using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;

namespace Sociam.Application.Features.Groups.Queries.GetGroupById;

public sealed class GetGroupByIdQuery : IRequest<Result<GroupListDto>>
{
    public Guid GroupId { get; set; }
}
