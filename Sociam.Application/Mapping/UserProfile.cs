using AutoMapper;
using Sociam.Application.DTOs.Users;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Application.Mapping;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserProfileDto>()
            .ForMember(dest => dest.JoinedAt, options => options.MapFrom(src => src.CreatedAt));
    }
}