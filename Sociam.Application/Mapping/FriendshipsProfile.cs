using AutoMapper;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Resolvers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Mapping;
public sealed class FriendshipsProfile : Profile
{
    public FriendshipsProfile()
    {
        CreateMap<Friendship, PendingFriendshipRequest>()
            .ForMember(dest => dest.RecipientName,
                options => options.MapFrom(src => string.Concat(src.Receiver.FirstName, " ", src.Receiver.LastName)))
            .ForMember(dest => dest.SenderName,
                options => options.MapFrom(src => string.Concat(src.Requester.FirstName, " ", src.Requester.LastName)))
            .ForMember(dest => dest.RequestedAt, options => options.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.FriendRequestId, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, options => options.MapFrom(src => src.FriendshipStatus))
            .ForMember(dest => dest.SenderProfilePicture,
                options => options.MapFrom<SenderProfilePictureUrlValueResolver>())
            .ForMember(dest => dest.RecipientProfilePicture,
                options => options.MapFrom<RecipientProfilePictureUrlValueResolver>())
            .ForMember(dest => dest.SenderId, options => options.MapFrom(src => src.RequesterId))
            .ForMember(dest => dest.RecipientId, options => options.MapFrom(src => src.ReceiverId));

        CreateMap<Friendship, GetUserAcceptedFriendshipDto>()
            .ForMember(dest => dest.FriendName, options =>
                options.MapFrom(src => string.Concat(src.Requester.FirstName, " ", src.Requester.LastName)))
            .ForMember(dest => dest.AcceptedAt, options =>
                options.MapFrom(src => src.CreatedAt));
    }
}
