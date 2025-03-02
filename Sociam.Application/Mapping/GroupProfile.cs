﻿using AutoMapper;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;

public sealed class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Group, GroupListDto>()
            .ForMember(destination => destination.CoverUrl, options => options.MapFrom<GroupCoverUrlValueResolver>());

        CreateMap<Group, GroupDto>()
           .ForMember(destination => destination.CoverUrl, options => options.MapFrom<GroupCoverUrlValueResolver>())
           .ForMember(destination => destination.Privacy, options => options.MapFrom(src => src.GroupPrivacy));
    }
}
