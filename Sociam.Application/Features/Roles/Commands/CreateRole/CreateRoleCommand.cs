using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Commands.CreateRole;
public sealed class CreateRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
}
