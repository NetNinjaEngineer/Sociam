﻿using AutoMapper;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Mapping;
public sealed class StoryProfile : Profile
{
    public StoryProfile()
    {
        CreateMap<MediaStory, MediaStoryDto>()
            .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType.ToString()))
            .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom<StoryMediaUrlValueResolver>())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom<MediaStoryCreatedAtTimeZoneConverterValueResolver>())
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom<MediaStoryExpiredAtTimeZoneConverterValueResolver>());


        CreateMap<TextStory, TextStoryDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom<TextStoryCreatedAtTimeZoneConverterValueResolver>())
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom<TextStoryExpiredAtTimeZoneConverterValueResolver>());

        CreateMap<Story, StoryDto>()
            .ForMember(dest => dest.StoryType, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom<StoryCreatedAtTimeZoneConverterValueResolver>())
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom<StoryExpiredAtTimeZoneConverterValueResolver>())
            .Include<TextStory, StoryDto>()
            .Include<MediaStory, StoryDto>();


        CreateMap<TextStory, StoryDto>()
            .IncludeBase<Story, StoryDto>()
            .ForMember(dest => dest.StoryType, opt => opt.MapFrom(_ => "text"))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.HashTags, opt => opt.MapFrom(src => src.HashTags));

        CreateMap<MediaStory, StoryDto>()
            .IncludeBase<Story, StoryDto>()
            .ForMember(dest => dest.StoryType, opt => opt.MapFrom(_ => "media"))
            .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Caption))
            .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.MediaUrl))
            .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => src.MediaType));

    }
}