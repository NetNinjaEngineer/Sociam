using AutoMapper;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Resolvers;

public sealed class StoryExpiredAtTimeZoneConverterValueResolver : IValueResolver<Story, StoryDto, DateTimeOffset>
{
    public DateTimeOffset Resolve(Story source, StoryDto destination, DateTimeOffset destMember, ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId);
    }
}