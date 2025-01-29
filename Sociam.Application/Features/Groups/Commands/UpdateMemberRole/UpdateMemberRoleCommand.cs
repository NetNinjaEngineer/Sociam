using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Groups.Commands.UpdateMemberRole;

public sealed class UpdateMemberRoleCommand : IRequest<Result<bool>>
{
    public Guid MemberId { get; set; }
    public Guid GroupId { get; set; }
    public GroupMemberRole Role { get; set; }
}
