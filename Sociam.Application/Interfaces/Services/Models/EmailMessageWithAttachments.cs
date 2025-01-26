using Microsoft.AspNetCore.Http;

namespace Sociam.Application.Interfaces.Services.Models;

public sealed class EmailMessageWithAttachments : EmailMessage
{
    public IEnumerable<IFormFile> Attachments { get; set; } = [];
}