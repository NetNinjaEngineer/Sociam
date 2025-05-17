using AutoMapper;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;

public sealed class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostCommand, Post>();
        CreateMap<PostLocationDto, PostLocation>().ReverseMap();
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.CreatedBy,
                options => options.MapFrom(src => string.Concat(src.CreatedBy.FirstName, " ", src.CreatedBy.LastName)));

        CreateMap<PostReaction, PostReactionDto>()
            .ForMember(dest => dest.ReactionId, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.ReactedBy.UserName))
            .ForMember(dest => dest.DisplayName,
                options => options.MapFrom(src =>
                    string.Concat(src.ReactedBy.FirstName, " ", src.ReactedBy.LastName)))
            .ForMember(dest => dest.ProfilePictureUrl,
                options => options.MapFrom(src => src.ReactedBy.ProfilePictureUrl))
            .ForMember(dest => dest.ReactedAt,
                options => options.MapFrom<PostReactionReactedAtTimeZoneValueResolver>())
            .ForMember(dest => dest.RelativeTime,
                options => options.MapFrom<PostReactionRelativeTimeValueResolver>());


    }
}
