// <copyright file="LogsProcessor.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using Microsoft.AspNetCore.Http;
using OpenTelemetry.Logs;

namespace Instrumentation.Processors;

/// <inheritdoc />
public class LogsProcessor : CoralogixProcessor<LogRecord>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogsProcessor"/> class.
    /// </summary>
    /// <param name="contextAccessor">Context accessor.</param>
    public LogsProcessor(IHttpContextAccessor contextAccessor)
        : base(contextAccessor)
    {
    }

    /// <inheritdoc/>
    protected override void AddAttributes(LogRecord data, params (string Key, string? Value)[] attributes)
    {
        var originalAttributes = data.Attributes ?? Enumerable.Empty<KeyValuePair<string, object?>>();
        List<KeyValuePair<string, object?>> newAttributes = [.. originalAttributes];
        newAttributes.AddRange(attributes.Select(a => KeyValuePair.Create(a.Key, (object?)(a.Value ?? "-------"))));
        data.Attributes = newAttributes;
    }
}