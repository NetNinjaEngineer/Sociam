using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.CreateNewGroup;
public sealed class CreateNewGroupCommandHandler(IGroupService groupService) : IRequestHandler<CreateNewGroupCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateNewGroupCommand request,
                                           CancellationToken cancellationToken) => await groupService.CreateNewGroupAsync(request);
}
