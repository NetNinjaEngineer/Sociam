using AutoMapper;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class TextStoryCreatedAtTimeZoneConverterValueResolver : IValueResolver<TextStory, TextStoryDto, DateTimeOffset>
{
    public DateTimeOffset Resolve(TextStory source, TextStoryDto destination, DateTimeOffset destMember, ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId);
    }
}