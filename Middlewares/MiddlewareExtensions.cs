using Microsoft.AspNetCore.Builder;
using Middlewares;

namespace Services.Middlewares;

/// <summary>
/// Extensions for middlewares.
/// </summary>


public static class MiddlewareExtensions
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<RequestIdMiddleware>();
        return app;
    }
}