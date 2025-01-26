using AutoMapper;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class StoryProfile : Profile
{
    public StoryProfile()
    {
        CreateMap<CreateStoryCommand, Story>();
        CreateMap<Story, StoryDto>()
            .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType.ToString()))
            .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom<StoryMediaUrlValueResolver>());
    }
}
