using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.AddUserToGroup;
public sealed class AddUserToGroupCommandHandler(IGroupService groupService) : IRequestHandler<AddUserToGroupCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        => await groupService.AddUserToGroupAsync(request);
}
