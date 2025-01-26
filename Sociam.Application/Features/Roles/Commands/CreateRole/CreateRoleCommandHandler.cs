using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.CreateRole;
public sealed class CreateRoleCommandHandler
    (IRoleService roleService) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => await roleService.CreateRole(request);
}
