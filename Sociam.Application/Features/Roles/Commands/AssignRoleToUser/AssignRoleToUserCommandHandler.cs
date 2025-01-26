using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.AssignRoleToUser;
public sealed class AssignRoleToUserCommandHandler
    (IRoleService roleService) : IRequestHandler<AssignRoleToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignRoleToUserCommand request,
        CancellationToken cancellationToken)
        => await roleService.AddRoleToUser(request);
}
