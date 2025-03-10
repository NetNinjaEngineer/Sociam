using FluentValidation;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Sociam.Api.Utils;

namespace Sociam.Api.Middleware;

internal sealed class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception? error)
    {
        var errorResponse = new GlobalErrorResponse();

        var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();

        switch (error)
        {
            case ValidationException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                errorResponse.Type = "Validation_Error";
                errorResponse.Message = "One or more validation errors occurred.";
                errorResponse.Errors = validationException.Errors
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                break;

            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Type = "Unauthorized";
                errorResponse.Message = "You are not authorized to perform this action.";
                break;

            case BadHttpRequestException badRequestException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Type = "Bad_Request";
                errorResponse.Message = badRequestException.Message;
                break;

            case InvalidOperationException invalidOpException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.Type = "Invalid_Operation";
                errorResponse.Message = invalidOpException.Message;
                break;

            case DbUpdateException dbUpdateException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.Type = "Database_Error";
                errorResponse.Message = "A database error occurred while processing your request.";
                errorResponse.Detail = env.IsDevelopment() ? dbUpdateException.InnerException?.Message : null;

                break;

            case InvalidJwtException invalidJwtException:
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Type = "Google_Authentication_Error";
                errorResponse.Message = "Invalid jwt";
                errorResponse.Detail = env.IsDevelopment() ? invalidJwtException.Message : null;
                break;

            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Type = "Internal_Server_Error";
                errorResponse.Message = "An unexpected error occurred while processing your request.";
                errorResponse.Detail = env.IsDevelopment() ? error?.Message : null;
                break;
        }
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(errorResponse, CancellationToken.None);
    }
}
