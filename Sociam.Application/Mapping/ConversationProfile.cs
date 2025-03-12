using AutoMapper;
using Sociam.Application.DTOs.Conversation;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class ConversationProfile : Profile
{
    public ConversationProfile()
    {
        CreateMap<Conversation, ConversationDto>()
            .ForMember(dest => dest.ConversationType, opt => opt.Ignore())
            .Include<PrivateConversation, ConversationDto>()
            .Include<GroupConversation, ConversationDto>();

        CreateMap<PrivateConversation, ConversationDto>()
            .ForMember(dest => dest.ConversationType, options => options.MapFrom(_ => "private"))
            .ForMember(dest => dest.Sender,
                options => options.MapFrom(src =>
                    string.Concat(src.SenderUser.FirstName, " ", src.SenderUser.LastName)))
            .ForMember(dest => dest.Recepient,
                options => options.MapFrom(src =>
                    string.Concat(src.ReceiverUser.FirstName, " ", src.ReceiverUser.LastName)))
            .ForMember(dest => dest.RecepientId, options => options.MapFrom(src => src.ReceiverUserId))
            .ForMember(dest => dest.SenderId, options => options.MapFrom(src => src.SenderUserId))
            .IncludeBase<Conversation, ConversationDto>();

        CreateMap<GroupConversation, ConversationDto>()
            .ForMember(dest => dest.ConversationType, options => options.MapFrom(_ => "group"))
            .ForMember(dest => dest.GroupId, options => options.MapFrom(src => src.GroupId))
            .IncludeBase<Conversation, ConversationDto>();

    }
}
