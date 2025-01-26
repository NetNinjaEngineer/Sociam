using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.SendPrivateMessageByCurrentUser;
public sealed class SendPrivateMessageByCurrentUserCommandHandler(
    IMessageService messageService) : IRequestHandler<SendPrivateMessageByCurrentUserCommand, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(
        SendPrivateMessageByCurrentUserCommand request, CancellationToken cancellationToken)
        => await messageService.SendPrivateMessageByCurrentUserAsync(request);
}
