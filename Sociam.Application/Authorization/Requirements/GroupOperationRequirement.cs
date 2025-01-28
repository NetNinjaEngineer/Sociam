using Microsoft.AspNetCore.Authorization;

namespace Sociam.Application.Authorization.Requirements
{
    public sealed class GroupOperationRequirement(GroupOperation operation) : IAuthorizationRequirement
    {
        public GroupOperation Operation { get; } = operation;
    }
}
