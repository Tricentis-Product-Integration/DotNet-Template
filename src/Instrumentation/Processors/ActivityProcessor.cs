// <copyright file="ActivityProcessor.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Instrumentation.Processors;

/// <inheritdoc />
public class ActivityProcessor : CoralogixProcessor<Activity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivityProcessor"/> class.
    /// </summary>
    /// <param name="contextAccessor">Context accessor.</param>
    public ActivityProcessor(IHttpContextAccessor contextAccessor)
        : base(contextAccessor)
    {
    }

    /// <inheritdoc/>
    protected override void AddAttributes(Activity data, params (string Key, string? Value)[] attributes)
    {
        foreach ((var key, var value) in attributes.Where(t => t.Value != null))
        {
            data.AddTag(key, value);
        }
    }
}
