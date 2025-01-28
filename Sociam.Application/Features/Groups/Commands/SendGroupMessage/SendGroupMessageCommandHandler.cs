using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Groups.Commands.SendGroupMessage
{
    public sealed class SendGroupMessageCommandHandler(
        IGroupService groupService) : IRequestHandler<SendGroupMessageCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(SendGroupMessageCommand request, CancellationToken cancellationToken)
        {
            return await groupService.SendGroupMessageAsync(request);
        }
    }
}
