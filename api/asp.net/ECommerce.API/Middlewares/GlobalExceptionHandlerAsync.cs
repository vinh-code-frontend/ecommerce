using ECommerce.API.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middlewares;

public class GlobalExceptionHandlerAsync
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerAsync> _logger;
    public GlobalExceptionHandlerAsync(RequestDelegate next, ILogger<GlobalExceptionHandlerAsync> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // _logger.LogInformation("Processing request: {Method} {Path}", context.Request.Method, context.Request.Path);
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message = exception.Message;

        switch (exception)
        {
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                break;

            case BadRequestException:
                status = HttpStatusCode.BadRequest;
                break;

            case ConflictException:
                status = HttpStatusCode.Conflict;
                break;

            case UnauthorizedException:
                status = HttpStatusCode.Unauthorized;
                break;

            case ForbiddenException:
                status = HttpStatusCode.Forbidden;
                break;

            default:
                status = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        var errorResponse = new
        {
            success = false,
            status = (int)status,
            message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(errorResponse)
        );
    }
}

