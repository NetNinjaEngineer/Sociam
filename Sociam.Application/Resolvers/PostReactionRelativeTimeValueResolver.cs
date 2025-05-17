using AutoMapper;
using Humanizer;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class PostReactionRelativeTimeValueResolver : IValueResolver<PostReaction, PostReactionDto, string>
{
    public string Resolve(PostReaction source, PostReactionDto destination, string destMember, ResolutionContext context)
    {
        var timeZoneId = context.Items["TimeZoneId"] as string;
        return source.ReactedAt.ConvertToUserLocalTimeZone(timeZoneId).Humanize();
    }
}