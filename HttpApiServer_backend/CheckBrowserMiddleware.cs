namespace HttpApiServer_backend;

public class CheckBrowserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public CheckBrowserMiddleware(RequestDelegate next,                                                                                                              
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.Log(LogLevel.Warning, context.Request.Headers.UserAgent);
        if (context.Request.Headers.UserAgent.ToString().Contains("Edg"))
        {
            await _next(context);
            _logger.Log(LogLevel.Information, "Is not Edge");
        }
        else
        {
            await context.Response.WriteAsync("Edge dont Support");
            _logger.Log(LogLevel.Warning,  "Edge dont support");
        }
    }  
}