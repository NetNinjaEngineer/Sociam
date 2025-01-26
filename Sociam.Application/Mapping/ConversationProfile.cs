using AutoMapper;
using Sociam.Application.DTOs.Conversation;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class ConversationProfile : Profile
{
    public ConversationProfile()
    {
        CreateMap<PrivateConversation, ConversationDto>()
            .ForMember(dest => dest.Sender, options => options.MapFrom(src => string.Concat(src.SenderUser.FirstName, " ", src.SenderUser.LastName)))
            .ForMember(dest => dest.Receiver, options => options.MapFrom(src => string.Concat(src.ReceiverUser.FirstName, " ", src.ReceiverUser.LastName)));

    }
}
