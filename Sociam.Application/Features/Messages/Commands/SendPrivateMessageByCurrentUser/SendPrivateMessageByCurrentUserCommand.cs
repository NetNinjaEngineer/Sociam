using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Commands.SendPrivateMessageByCurrentUser;
public sealed class SendPrivateMessageByCurrentUserCommand : IRequest<Result<MessageDto>>
{
    public Guid ConversationId { get; set; }
    public string ReceiverId { get; set; } = null!;
    public string? Content { get; set; }

    public IEnumerable<IFormFile>? Attachments = [];
}
