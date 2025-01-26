using Microsoft.AspNetCore.Mvc;

namespace Sociam.Api.Utils;
public sealed class GlobalErrorResponse : ProblemDetails
{
    public string? Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
