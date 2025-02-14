using AutoMapper;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class MediaStoryExpiredAtTimeZoneConverterValueResolver : IValueResolver<MediaStory, MediaStoryDto, DateTimeOffset>
{
    public DateTimeOffset Resolve(MediaStory source, MediaStoryDto destination, DateTimeOffset destMember, ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId);
    }
}