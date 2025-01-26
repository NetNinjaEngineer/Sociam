using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Commands.SendPrivateMessage;
public sealed class SendPrivateMessageCommand : IRequest<Result<MessageDto>>
{
    public Guid ConversationId { get; set; }
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
    public string? Content { get; set; }

    public IEnumerable<IFormFile>? Attachments = [];
}
