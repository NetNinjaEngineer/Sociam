using Microsoft.AspNetCore.Http;

namespace Sociam.Application.DTOs.Messages
{
    public sealed class SendGroupMessageDto
    {
        public string? Content { get; set; }
        public ICollection<IFormFile>? Attachments { get; set; }
    }
}
