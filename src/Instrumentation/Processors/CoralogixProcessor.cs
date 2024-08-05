// <copyright file="CoralogixProcessor.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using Common.Configuration;
using Microsoft.AspNetCore.Http;
using OpenTelemetry;

namespace Instrumentation.Processors;

/// <inheritdoc />
public abstract class CoralogixProcessor<T> : BaseProcessor<T>
{
    private readonly IHttpContextAccessor contextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoralogixProcessor{T}"/> class.
    /// </summary>
    /// <param name="contextAccessor">Context accessor.</param>
    public CoralogixProcessor(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    /// <inheritdoc />
    public override void OnEnd(T data)
    {
        string? reqId = null;
        string? requestMethod = null;
        string? traceId = null;
        string? error = null;

        var context = this.contextAccessor.HttpContext;
        if (context != null)
        {
            if (context.Items.TryGetValue("RequestId", out var req))
            {
                reqId = req!.ToString()!;
            }

            if (context.Items.TryGetValue("Error", out var err))
            {
                error = err!.ToString();
            }

            requestMethod = context.Request.Method;
            traceId = context.TraceIdentifier;

        }

        var attributes = new List<(string Key, string? Value)>
        {
            ("request-id", reqId),
            ("request-method", requestMethod),
            ("trace-id", traceId),
            ("error", error)
        };
        if (!Environments.IsProduction)
        {
            attributes.Add(("env", Environments.CurrentEnvironment));
        }

        this.AddAttributes(data, attributes.ToArray());
        base.OnEnd(data);
    }

    /// <summary>
    /// Adds attributes to data.
    /// </summary>
    /// <param name="data">Data to modify.</param>
    /// <param name="attributes">Attributes.</param>
    protected abstract void AddAttributes(T data, params (string Key, string? Value)[] attributes);
}
