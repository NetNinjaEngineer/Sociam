using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Commands.AssignRoleToUser;
public sealed class AssignRoleToUserCommand : IRequest<Result<string>>
{
    public string UserId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
