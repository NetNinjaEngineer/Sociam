using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.EditRole;
public sealed class EditRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<EditRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        EditRoleCommand request,
        CancellationToken cancellationToken)
        => await roleService.EditRole(request);
}
