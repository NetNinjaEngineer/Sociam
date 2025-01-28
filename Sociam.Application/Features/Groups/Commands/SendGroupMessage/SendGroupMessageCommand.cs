using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Groups.Commands.SendGroupMessage
{
    public sealed class SendGroupMessageCommand : IRequest<Result<Guid>>
    {
        public Guid GroupId { get; set; }
        public Guid GroupConversationId { get; set; }
        public string? Content { get; set; }
        public ICollection<IFormFile>? Attachments { get; set; }
    }
}
