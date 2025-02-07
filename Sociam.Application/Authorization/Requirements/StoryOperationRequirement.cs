using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Helpers;

namespace Sociam.Application.Authorization.Requirements;

public sealed class StoryOperationRequirement(StoryOperation operation) : IAuthorizationRequirement
{
    public StoryOperation StoryOperation { get; set; } = operation;
}