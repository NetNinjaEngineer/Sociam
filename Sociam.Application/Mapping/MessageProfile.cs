using AutoMapper;
using Sociam.Application.DTOs.Attachments;
using Sociam.Application.DTOs.Mentions;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.DTOs.Reactions;
using Sociam.Application.DTOs.Replies;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Attachment, AttachmentDto>();
        CreateMap<MessageMention, MessageMentionDto>();
        CreateMap<MessageReaction, MessageReactionDto>();
        CreateMap<MessageReply, MessageReplyDto>()
            .ForMember(dest => dest.ReplyId, options => options.MapFrom(src => src.Id));

        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderName, options => options.MapFrom(
                src => string.Concat(src.Sender.FirstName, " ", src.Sender.LastName)));
    }
}
