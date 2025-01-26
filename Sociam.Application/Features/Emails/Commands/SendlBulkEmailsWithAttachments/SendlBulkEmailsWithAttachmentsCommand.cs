using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Emails.Commands.SendlBulkEmailsWithAttachments;
public sealed class SendlBulkEmailsWithAttachmentsCommand : IRequest<Result<bool>>
{
    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public List<string> ToReceipients { get; set; } = [];

    public List<IFormFile> Attachments { get; set; } = [];
}
