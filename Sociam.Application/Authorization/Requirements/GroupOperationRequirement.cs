using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Helpers;

namespace Sociam.Application.Authorization.Requirements
{
    public sealed class GroupOperationRequirement(GroupOperation operation) : IAuthorizationRequirement
    {
        public GroupOperation Operation { get; } = operation;
    }
}
