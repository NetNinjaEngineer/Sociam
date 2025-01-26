using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Emails.Commands.SendEmailWithAttachments;
public sealed class SendEmailWithAttachmentsCommand : IRequest<Result<bool>>
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public IEnumerable<IFormFile> Attachments { get; set; } = [];
}
