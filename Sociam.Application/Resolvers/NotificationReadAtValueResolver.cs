using AutoMapper;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class NotificationReadAtValueResolver : IValueResolver<Notification, NotificationDto, DateTimeOffset?>
{
    public DateTimeOffset? Resolve(Notification source, NotificationDto destination, DateTimeOffset? destMember,
        ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.ReadAt?.ConvertToUserLocalTimeZone(timeZoneId);
    }
}