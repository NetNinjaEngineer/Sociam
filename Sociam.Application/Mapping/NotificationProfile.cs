using AutoMapper;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;

public sealed class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationDto>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.NKind, options => options.Ignore())
            .ForMember(dest => dest.SenderId, options => options.MapFrom(src => src.ActorId))
            .ForMember(dest => dest.SenderName,
                options => options.MapFrom(src => $"{src.Actor.FirstName} {src.Actor.LastName}"))
            .ForMember(dest => dest.RecipientName,
                options => options.MapFrom(src => $"{src.Recipient.FirstName} {src.Recipient.LastName}"))
            .Include<PostNotification, NotificationDto>()
            .Include<GroupNotification, NotificationDto>()
            .Include<NetworkNotification, NotificationDto>()
            .Include<MediaNotification, NotificationDto>()
            .Include<StoryNotification, NotificationDto>()
            .ForMember(dest => dest.CreatedAt, options => options.MapFrom<NotificationCreatedAtValueResolver>())
            .ForMember(dest => dest.ReadAt, options => options.MapFrom<NotificationReadAtValueResolver>());


        CreateMap<PostNotification, NotificationDto>()
            .IncludeBase<Notification, NotificationDto>()
            .ForMember(dest => dest.NKind, options => options.MapFrom(_ => "post"))
            .ForMember(dest => dest.PostId, options => options.MapFrom(src => src.PostId))
            .ForMember(dest => dest.PostContent, options => options.MapFrom(src => src.PostContent));

        CreateMap<GroupNotification, NotificationDto>()
            .IncludeBase<Notification, NotificationDto>()
            .ForMember(dest => dest.NKind, options => options.MapFrom(_ => "group"))
            .ForMember(dest => dest.GroupId, options => options.MapFrom(src => src.GroupId))
            .ForMember(dest => dest.GroupName, options => options.MapFrom(src => src.GroupName))
            .ForMember(dest => dest.GroupRole, options => options.MapFrom(src => src.GroupRole));

        CreateMap<NetworkNotification, NotificationDto>()
            .IncludeBase<Notification, NotificationDto>()
            .ForMember(dest => dest.NKind, options => options.MapFrom(_ => "network"));

        CreateMap<MediaNotification, NotificationDto>()
            .IncludeBase<Notification, NotificationDto>()
            .ForMember(dest => dest.NKind, options => options.MapFrom(_ => "media"))
            .ForMember(dest => dest.MediaId, options => options.MapFrom(src => src.MediaId))
            .ForMember(dest => dest.MediaNotificationType, options => options.MapFrom(src => src.MediaNotificationType));

        CreateMap<StoryNotification, NotificationDto>()
            .IncludeBase<Notification, NotificationDto>()
            .ForMember(dest => dest.NKind, options => options.MapFrom(_ => "story"))
            .ForMember(dest => dest.StoryId, options => options.MapFrom(src => src.StoryId))
            .ForMember(dest => dest.Privacy, options => options.MapFrom(src => src.Privacy));
    }
}