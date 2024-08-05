// <copyright file="ServiceBuilder.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using Instrumentation.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Instrumentation;

/// <summary>
/// Extensions for instrumentation configuration.
/// </summary>
public static class ServiceBuilder
{
    /// <summary>
    /// Add instrumentation to the service.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="builder">The instrumentation builders.</param>
    /// <returns>Modified collection.</returns>
    public static IServiceCollection AddInstrumentation(this IServiceCollection services)
    {
        var attributes = new Dictionary<string, object> { ["cx.subsystem.name"] = Instrumentation.ServiceName };

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.Clear()
                .AddService(Instrumentation.ServiceName, serviceVersion: Instrumentation.ServiceVersion)
                .AddAttributes(attributes))
            .WithTracing(ConfigureTracing)
            .WithMetrics(ConfigureMetrics);

            services.AddLogging(b => b.ClearProviders()
                .AddOpenTelemetry(ConfigureLogs))
                .AddOpenTelemetry();

        services.AddSingleton<LogsProcessor>();
        services.AddSingleton<ConsoleLogsExporter>();
        services.AddSingleton<IInstrumentation, Instrumentation>();
        return services;

        void ConfigureTracing(TracerProviderBuilder tracing)
        {
            tracing.ConfigureResource(r => r.AddAttributes(attributes))
                .AddSource(Instrumentation.ServiceName)
                .AddProcessor<ActivityProcessor>()
                .SetSampler<AlwaysOnSampler>();
        }

        void ConfigureLogs(OpenTelemetryLoggerOptions logs)
        {
            logs.IncludeFormattedMessage = true;
            logs.AddProcessor(p => p.GetRequiredService<LogsProcessor>())
                .SetResourceBuilder(ResourceBuilder.CreateEmpty()
                    .AddService(Instrumentation.ServiceName, serviceVersion: Instrumentation.ServiceVersion)
                    .AddAttributes(attributes))
                .AddProcessor(p => new SimpleLogRecordExportProcessor(p.GetRequiredService<ConsoleLogsExporter>()));
        }

        void ConfigureMetrics(MeterProviderBuilder metrics)
        {
            metrics.ConfigureResource(r => r.AddAttributes(attributes))
                .AddMeter(Instrumentation.ServiceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation();
        }
    }
}
