

namespace Notifications.gRPC.Middleware;

public class GetHostMiddleware
{
    private readonly RequestDelegate _next;
    private readonly GlobalOptions _options;
    public GetHostMiddleware(RequestDelegate next, GlobalOptions globalOptions)
    {
        _next = next;
        _options = globalOptions;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _options.HostName = context.Request.Host.Host + ":" + context.Request.Host.Port;

        await _next.Invoke(context);
    }
}
