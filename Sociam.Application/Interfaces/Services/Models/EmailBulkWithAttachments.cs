using Microsoft.AspNetCore.Http;

namespace Sociam.Application.Interfaces.Services.Models;

public sealed class EmailBulkWithAttachments : BaseEmailMessage
{
    public List<string> ToReceipients { get; set; } = [];
    public List<IFormFile> Attachments { get; set; } = [];
}