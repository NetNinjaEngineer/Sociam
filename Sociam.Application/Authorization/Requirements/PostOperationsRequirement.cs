using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Helpers;

namespace Sociam.Application.Authorization.Requirements
{
    public sealed class PostOperationsRequirement(PostOperations postOperations) : IAuthorizationRequirement
    {
        public PostOperations PostOperations { get; private set; } = postOperations;
    }
}
