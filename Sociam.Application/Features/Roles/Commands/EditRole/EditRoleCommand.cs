using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Commands.EditRole;
public sealed class EditRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
    public string NewRoleName { get; set; } = string.Empty;
}
