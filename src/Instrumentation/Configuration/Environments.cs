// <copyright file="Environments.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

namespace Common.Configuration;

/// <summary>
/// Provides environment recognition.
/// </summary>
public static class Environments
{
    /// <summary>
    /// The current environment.
    /// </summary>
    public static readonly string CurrentEnvironment;

    /// <summary>
    /// Is environment local.
    /// </summary>
    public static readonly bool IsLocal;

    /// <summary>
    /// Is environment development.
    /// </summary>
    public static readonly bool IsDevelopment;

    /// <summary>
    /// Is environment staging.
    /// </summary>
    public static readonly bool IsStaging;

    /// <summary>
    /// Is environment production.
    /// </summary>
    public static readonly bool IsProduction;

    private static readonly string EnvironmentVariable = "ASPNETCORE_ENVIRONMENT";
    private static readonly string Local = "local";
    private static readonly string Development = "development";
    private static readonly string Staging = "staging";
    private static readonly string Production = "production";

    static Environments()
    {
        CurrentEnvironment = (Environment.GetEnvironmentVariable(EnvironmentVariable) ?? throw new ArgumentNullException(EnvironmentVariable, "Environment not assigned.")).ToLowerInvariant();
        IsLocal = string.Equals(CurrentEnvironment, Local, StringComparison.Ordinal);
        IsDevelopment = string.Equals(CurrentEnvironment, Development, StringComparison.Ordinal);
        IsStaging = string.Equals(CurrentEnvironment, Staging, StringComparison.Ordinal);
        IsProduction = string.Equals(CurrentEnvironment, Production, StringComparison.Ordinal);
    }
}
