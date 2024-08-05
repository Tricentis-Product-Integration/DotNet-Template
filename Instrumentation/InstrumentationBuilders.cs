// <copyright file="InstrumentationBuilders.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Instrumentation;

/// <summary>
/// Provides builders for instrumentation.
/// </summary>
public class InstrumentationBuilders
{
    /// <summary>
    /// Gets the tracer builder.
    /// </summary>
    public Func<TracerProviderBuilder, TracerProviderBuilder>? TracerBuilder { get; init; }

    /// <summary>
    /// Gets the metrics build.
    /// </summary>
    public Func<MeterProviderBuilder, MeterProviderBuilder>? MetricsBuilder { get; init; }

    /// <summary>
    /// Gets the logs builder.
    /// </summary>
    public Func<OpenTelemetryLoggerOptions, OpenTelemetryLoggerOptions>? LogsBuilder { get; init; }
}
