using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.AssignClaimToUser;
public sealed class AssignClaimToUserCommandHandler(IRoleService roleService)
    : IRequestHandler<AssignClaimToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignClaimToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AddClaimToUser(request);
}
