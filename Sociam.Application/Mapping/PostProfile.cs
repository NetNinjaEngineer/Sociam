using AutoMapper;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping
{
    public sealed class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostCommand, Post>();

            CreateMap<PostLocationDto, PostLocation>();
        }
    }
}
