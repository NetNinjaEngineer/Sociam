using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.DeleteRole;
public sealed class DeleteRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<DeleteRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        DeleteRoleCommand request,
        CancellationToken cancellationToken)
        => await roleService.DeleteRole(request);
}
