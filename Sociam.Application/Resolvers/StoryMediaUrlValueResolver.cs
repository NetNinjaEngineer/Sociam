using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sociam.Application.DTOs.Stories;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Application.Resolvers;
public sealed class StoryMediaUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<MediaStory, MediaStoryDto, string?>
{
    public string Resolve(MediaStory source, MediaStoryDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.MediaUrl))
            return string.Empty;

        if (source.MediaType is not (MediaType.Image or MediaType.Video))
            return string.Empty;

        var subFolder = source.MediaType == MediaType.Image ? "Images" : "Videos";

        return contextAccessor.HttpContext.Request.IsHttps
            ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/{subFolder}/{source.MediaUrl}"
            : $"{configuration["FullbackUrl"]}/Uploads/Stories/{subFolder}/{source.MediaUrl}";
    }
}
