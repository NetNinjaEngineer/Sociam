using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Posts.Commands.CreatePost
{
    public sealed class CreatePostCommand : IRequest<Result<Guid>>
    {
        public string? Text { get; set; }
        public PostPrivacy Privacy { get; set; } = PostPrivacy.Public;
        public PostFeeling Feeling { get; set; } = PostFeeling.None;
        public IFormFileCollection? Media { get; set; }
        public List<string>? TaggedUserIds { get; set; }
        public PostLocationDto? Location { get; set; }
    }
}
