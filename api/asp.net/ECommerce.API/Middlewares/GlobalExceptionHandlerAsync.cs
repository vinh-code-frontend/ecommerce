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
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        }
    }
}
