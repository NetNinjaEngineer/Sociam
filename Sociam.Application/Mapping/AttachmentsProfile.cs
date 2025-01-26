using AutoMapper;
using Sociam.Application.DTOs.Attachments;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class AttachmentsProfile : Profile
{
    public AttachmentsProfile()
    {
        CreateMap<Attachment, AttachmentDto>()
            .ForMember(dest => dest.Type, options => options.MapFrom(src => src.AttachmentType))
            .ForMember(dest => dest.Size, options => options.MapFrom(src => src.AttachmentSize));
    }
}
