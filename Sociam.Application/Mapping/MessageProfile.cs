using AutoMapper;
using Sociam.Application.DTOs.Messages;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderName, options => options.MapFrom(
                src => string.Concat(src.PrivateConversation!.SenderUser.FirstName, " ", src.PrivateConversation!.SenderUser.LastName)))
            .ForMember(dest => dest.ReceiverName, options => options.MapFrom(
                src => string.Concat(src.PrivateConversation!.ReceiverUser.FirstName, " ", src.PrivateConversation!.ReceiverUser.LastName)));
    }
}
