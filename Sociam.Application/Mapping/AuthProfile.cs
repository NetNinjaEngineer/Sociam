using AutoMapper;
using Sociam.Application.Features.Auth.Commands.Register;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Application.Mapping;
public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterCommand, ApplicationUser>();
    }
}
