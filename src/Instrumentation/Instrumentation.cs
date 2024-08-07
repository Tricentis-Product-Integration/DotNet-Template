// <copyright file="Instrumentation.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using System.Diagnostics;
using System.Reflection;

namespace Instrumentation;

/// <inheritdoc />
public class Instrumentation : IInstrumentation
{
    /// <summary>
    /// Service name.
    /// </summary>
    internal static readonly string ServiceName = Assembly.GetEntryAssembly()!.GetName().Name!;

    /// <summary>
    /// Service version.
    /// </summary>
    internal static readonly string ServiceVersion = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();

    private ActivitySource source = new(ServiceName, ServiceVersion);

    /// <inheritdoc/>
    public Activity? StartActivity(string name, ActivityKind kind, params Tag[] tags)
    {
        return this.source.StartActivity(kind, name: name, tags: tags.Select(t => t.KeyValuePair).ToArray());
    }

    /// <inheritdoc/>
    public Activity? StartActivity(string name, ActivityKind kind, ActivityContext parentContext, Tag[] tags)
    {
        return this.source.StartActivity(kind, parentContext, name: name, tags: tags.Select(t => t.KeyValuePair).ToArray());
    }
}
