using Prova1.Application.Common.Errors;
using System.Net;
using System.Text.Json;

namespace Prova1.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleUnauthorizedAccessException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleUnauthorizedAccessException(HttpContext context, Exception exception)
    {
        HttpStatusCode code = HttpStatusCode.Unauthorized;

        string? result = JsonSerializer.Serialize(new
        {
            title = "Unauthorized Access",
            type = "",
            detail = exception.Message,
            status = (int)code,
            traceId = context.TraceIdentifier
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError;

        string? result = JsonSerializer.Serialize(new
        {
            title = "An error occurred.",
            type = "Internal server error.",
            detail = exception is IExceptionBase ? ((IExceptionBase)exception).ErrorMessage : string.Empty,
            status = (int)code,
            traceId = context.TraceIdentifier
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}