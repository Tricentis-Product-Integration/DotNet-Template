using Microsoft.AspNetCore.Http;

namespace Middleware.Managers;

public class RequestIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        return _next(httpContext);
    }
}

public static class RequestIdMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestIdMiddleware>();
    }
}
