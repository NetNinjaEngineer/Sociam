using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class SenderProfilePictureUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Friendship, PendingFriendshipRequest, string>
{
    public string Resolve(Friendship source, PendingFriendshipRequest destination, string destMember, ResolutionContext context)
    {
        var profilePictureUrl = source.Requester.ProfilePictureUrl;

        if (string.IsNullOrEmpty(profilePictureUrl))
            return string.Empty;

        return contextAccessor.HttpContext.Request.IsHttps
            ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{profilePictureUrl}"
            : $"{configuration["FullbackUrl"]}/Uploads/Images/{profilePictureUrl}";
    }
}