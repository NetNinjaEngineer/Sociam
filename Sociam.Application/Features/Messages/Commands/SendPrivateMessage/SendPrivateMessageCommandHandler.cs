using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.SendPrivateMessage;
public sealed class SendPrivateMessageCommandHandler(
    IMessageService messageService) : IRequestHandler<SendPrivateMessageCommand, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(SendPrivateMessageCommand request, CancellationToken cancellationToken)
        => await messageService.SendPrivateMessageAsync(request);
}
