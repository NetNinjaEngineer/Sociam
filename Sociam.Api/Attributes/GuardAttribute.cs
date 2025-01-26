using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Filters;

namespace Sociam.Api.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class GuardAttribute : TypeFilterAttribute
{
    public GuardAttribute(
        string[]? policies = null,
        string[]? roles = null) : base(typeof(GuardFilter))
    {
        Arguments = [policies ?? [], roles ?? []];
        Order = 2;
    }
}
