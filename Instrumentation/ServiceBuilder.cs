// <copyright file="ServiceBuilder.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using Common.Configuration;
using Common.Date;
using Instrumentation.Date;
using Instrumentation.Processors;
using Instrumentation.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Exporter;
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
    public static IServiceCollection AddInstrumentation(this IServiceCollection services, IConfiguration configuration, InstrumentationBuilders? builder = null)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IValidateOptions<TelemetrySettings>, TelemetrySettingsValidator>();
        services.BindOptions<TelemetrySettings>(configuration, TelemetrySettings.TelemetryKey);
        TelemetrySettings telemetrySettings = configuration.GetObject<TelemetrySettings>(TelemetrySettings.TelemetryKey);

        var attributes = new Dictionary<string, object> { ["cx.subsystem.name"] = Instrumentation.ServiceName, ["cx.application.name"] = telemetrySettings.ApplicationName! };

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.Clear().AddService(Instrumentation.ServiceName, serviceVersion: Instrumentation.ServiceVersion).AddAttributes(attributes))
            .WithTracing(ConfigureTracing)
            .WithMetrics(ConfigureMetrics);

        services.AddLogging(b => b.ClearProviders().AddOpenTelemetry(ConfigureLogs));

        services.AddSingleton<LogsProcessor>();
        services.AddSingleton<ConsoleLogsExporter>();
        services.AddSingleton<IInstrumentation, Instrumentation>();
        return services;

        void ConfigureTracing(TracerProviderBuilder tracing)
        {
            tracing.ConfigureResource(r => r.AddAttributes(attributes))
                .AddSource(Instrumentation.ServiceName)
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = context => !telemetrySettings.ExcludedPaths!.Any(e => context.Request.Path.StartsWithSegments(e));
                })
                .AddHttpClientInstrumentation(options =>
                {
                    options.FilterHttpRequestMessage = req => !telemetrySettings.ExcludedPaths!.Any(e => req.RequestUri?.PathAndQuery.Contains(e) ?? false);
                    options.FilterHttpWebRequest = req => !telemetrySettings.ExcludedPaths!.Any(e => req.RequestUri?.PathAndQuery.Contains(e) ?? false);
                })
                .AddProcessor<ActivityProcessor>()
                .SetSampler<AlwaysOnSampler>()
                .AddOtlpExporter(CreateOtlpBuilder(telemetrySettings.TracingEndpoint!));
            builder?.TracerBuilder?.Invoke(tracing);
        }

        void ConfigureLogs(OpenTelemetryLoggerOptions logs)
        {
            logs.IncludeFormattedMessage = true;
            logs.AddProcessor(p => p.GetRequiredService<LogsProcessor>())
                .SetResourceBuilder(ResourceBuilder.CreateEmpty().AddService(Instrumentation.ServiceName, serviceVersion: Instrumentation.ServiceVersion).AddAttributes(attributes))
                .AddProcessor(p => new SimpleLogRecordExportProcessor(p.GetRequiredService<ConsoleLogsExporter>()))
                .AddOtlpExporter(CreateOtlpBuilder(telemetrySettings.LogsEndpoint!));
            builder?.LogsBuilder?.Invoke(logs);
        }

        void ConfigureMetrics(MeterProviderBuilder metrics)
        {
            metrics.ConfigureResource(r => r.AddAttributes(attributes))
                .AddMeter(Instrumentation.ServiceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(CreateOtlpBuilder(telemetrySettings.MetricsEndpoint!));
            builder?.MetricsBuilder?.Invoke(metrics);
        }

        Action<OtlpExporterOptions> CreateOtlpBuilder(string endpoint) => otlp =>
        {
            otlp.Endpoint = new Uri(endpoint);
            otlp.Headers = $"Authorization=Bearer {telemetrySettings.ApiKey}";
        };
    }
}
