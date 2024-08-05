// <copyright file="IInstrumentation.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using System.Diagnostics;

namespace Instrumentation;

/// <summary>
/// Provides service-wide instrumentation.
/// </summary>
public interface IInstrumentation
{
    /// <summary>
    /// Starts a new activity.
    /// </summary>
    /// <param name="name">Activity name.</param>
    /// <param name="kind">Activity kind.</param>
    /// <param name="tags">Additional tags.</param>
    /// <returns>Activity.</returns>
    Activity? StartActivity(string name, ActivityKind kind, params Tag[] tags);

    /// <summary>
    /// Starts a new activity.
    /// </summary>
    /// <param name="name">Activity name.</param>
    /// <param name="kind">Activity kind.</param>
    /// <param name="parentContext">Activity context.</param>
    /// <param name="tags">Additional tags.</param>
    /// <returns>Activity.</returns>
    Activity? StartActivity(string name, ActivityKind kind, ActivityContext parentContext, params Tag[] tags);
}
