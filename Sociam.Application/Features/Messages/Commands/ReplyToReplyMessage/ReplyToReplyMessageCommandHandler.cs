using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.ReplyToReplyMessage;
public sealed class ReplyToReplyMessageCommandHandler(IMessageService service) :
    IRequestHandler<ReplyToReplyMessageCommand, Result<MessageReplyDto>>
{
    public async Task<Result<MessageReplyDto>> Handle(ReplyToReplyMessageCommand request, CancellationToken cancellationToken)
    {
        return await service.ReplyToReplyMessageAsync(request);
    }
}
