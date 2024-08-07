using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class BusinessServicesBuilder
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        return services.AddScoped<IDemoService, DemoService>();
    }
}