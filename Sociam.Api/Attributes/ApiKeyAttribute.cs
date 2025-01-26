using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Filters;

namespace Sociam.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyFilter))
    {
        Order = 1;
    }
}
