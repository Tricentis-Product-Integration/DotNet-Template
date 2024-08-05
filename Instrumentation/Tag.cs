// <copyright file="Tag.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

namespace Instrumentation;

/// <summary>
/// Tags for the instrumentation.
/// </summary>
public sealed class Tag
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tag"/> class.
    /// </summary>
    /// <param name="key">Key.</param>
    /// <param name="value">Value.</param>
    public Tag(string key, object? value)
    {
        this.KeyValuePair = new(key, value);
    }

    /// <summary>
    /// Gets keyValue.
    /// </summary>
    public KeyValuePair<string, object?> KeyValuePair { get; init; }
}
