using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Posts.Commands.EditPost
{
    public sealed class EditPostCommand : IRequest<Result<Unit>>
    {
        public Guid PostId { get; set; }
        public string? Content { get; set; }
        public PostPrivacy PostPrivacy { get; set; } = PostPrivacy.Public;
        public PostFeeling PostFeeling { get; set; } = PostFeeling.None;
        public PostLocationDto? PostLocation { get; set; }
        public IFormFileCollection? Media { get; set; }
        public List<string>? TaggedUserIds { get; set; }
    }
}
