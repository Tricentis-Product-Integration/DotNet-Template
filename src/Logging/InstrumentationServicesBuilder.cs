using Instrumentation.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Instrumentation;

public static class InstrumentationServicesBuilder
{
    public static IServiceCollection UseLogExporter(this IServiceCollection services)
    {
        /*
        services.AddHttpContextAccessor();
        services.AddOpenTelemetry();
        services.AddSingleton<LogsProcessor>();
        services.AddSingleton<ConsoleLogsExporter>();
        */
        return services;
    }
}

