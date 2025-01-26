using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Commands.DeleteRole;
public sealed class DeleteRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;

}
