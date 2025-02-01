using Microsoft.AspNetCore.Authorization;

namespace Sociam.Application.Authorization.Requirements;

public sealed class StoryOperationRequirement(StoryOperation operation) : IAuthorizationRequirement
{
    public StoryOperation StoryOperation { get; set; } = operation;
}