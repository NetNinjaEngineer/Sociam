﻿using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Commands.AddClaimToRole;
public sealed class AddClaimToRoleCommandHandler
    (IRoleService roleService) : IRequestHandler<AddClaimToRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddClaimToRoleCommand request, CancellationToken cancellationToken)
        => await roleService.AddClaimToRole(request);
}
