using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.JoinGroup;

public sealed class JoinGroupCommandHandler(IGroupService groupService) :
    IRequestHandler<JoinGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        JoinGroupCommand request, CancellationToken cancellationToken)
    {
        return await groupService.JoinGroupAsync(request);
    }
}
