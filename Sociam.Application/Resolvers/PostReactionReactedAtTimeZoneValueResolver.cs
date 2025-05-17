using AutoMapper;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class PostReactionReactedAtTimeZoneValueResolver : IValueResolver<PostReaction, PostReactionDto, DateTimeOffset>
{
    public DateTimeOffset Resolve(PostReaction source, PostReactionDto destination, DateTimeOffset destMember, ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.ReactedAt.ConvertToUserLocalTimeZone(timeZoneId);
    }
}