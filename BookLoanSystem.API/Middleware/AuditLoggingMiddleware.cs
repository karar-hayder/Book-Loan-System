public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditLoggingMiddleware> _logger;

    public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestPath = context.Request.Path;
        var requestTime = DateTime.UtcNow;
        Console.WriteLine($"[Audit] {requestTime:O} - Request Path: {requestPath}");

        string? userId = null;
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            userId = context.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        if (!string.IsNullOrEmpty(userId))
        {
            _logger.LogInformation("Authenticated user id: {UserId} performed request to {Path}", userId, requestPath);
        }
        context.Response.OnStarting(() => {
            context.Response.Headers.Append("X-Audit-Logged", "true");
            return Task.CompletedTask;
        });

        await _next(context);
    }
}