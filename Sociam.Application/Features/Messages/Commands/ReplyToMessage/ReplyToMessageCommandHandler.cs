using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.ReplyToMessage;

public sealed class ReplyToMessageCommandHandler(IMessageService messageService)
    : IRequestHandler<ReplyToMessageCommand, Result<MessageReplyDto>>
{
    public async Task<Result<MessageReplyDto>> Handle(
        ReplyToMessageCommand request, CancellationToken cancellationToken)
        => await messageService.ReplyToPrivateMessageAsync(request);
}
