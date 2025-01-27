using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sociam.Application.DTOs.Groups;
using Sociam.Domain.Entities;

namespace Sociam.Application.Resolvers;

public sealed class GroupCoverUrlValueResolver(
    IConfiguration configuration,
    IHttpContextAccessor contextAccessor) : IValueResolver<Group, GroupListDto, string?>
{
    public string Resolve(Group source, GroupListDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.PictureName))
            return string.Empty;

        var groupsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Groups");

        if (!Directory.Exists(groupsFolderPath))
            Directory.CreateDirectory(groupsFolderPath);

        return contextAccessor.HttpContext.Request.IsHttps
                  ? $"{configuration["BaseApiUrl"]}/Uploads/Groups/{source.PictureName}"
                  : $"{configuration["FullbackUrl"]}/Uploads/Groups/{source.PictureName}";
    }
}