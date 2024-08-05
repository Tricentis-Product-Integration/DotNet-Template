// <copyright file="ConsoleLogsExporter.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using Common.Extensions;
using Instrumentation.Date;
using OpenTelemetry;
using OpenTelemetry.Logs;

namespace Instrumentation;

/// <summary>
/// Exports logs to console.
/// </summary>
public class ConsoleLogsExporter : BaseExporter<LogRecord>
{
    private IDateTimeService dateTimeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleLogsExporter"/> class.
    /// </summary>
    /// <param name="dateTimeService">dateTimeService.</param>
    public ConsoleLogsExporter(IDateTimeService dateTimeService)
    {
        this.dateTimeService = dateTimeService;
    }

    /// <inheritdoc/>
    public override ExportResult Export(in Batch<LogRecord> batch)
    {
        using var scope = SuppressInstrumentationScope.Begin();
        foreach (var logRecord in batch)
        {
            var logLevel = logRecord.LogLevel.ToString();
            var props = new[]
            {
                this.dateTimeService.ConvertToString(DateTime.UtcNow),
                GetAttribute(logRecord, "request-id"),
                logLevel.Length == 5 ? logLevel : $"{logLevel.Substring(0, 4)} ",
                GetAttribute(logRecord, "tenant"),
                GetAttribute(logRecord, "product"),
                logRecord.CategoryName,
                logRecord.FormattedMessage,
            };
            Console.WriteLine(props!.JoinWith("\t"));
            if (logRecord.Exception != null)
            {
                Console.WriteLine(logRecord.Exception);
            }
        }

        return ExportResult.Success;

        string GetAttribute(LogRecord logRecord, string tenant)
        {
            return logRecord.Attributes!.FirstOrDefault(pair => pair.Key.Equals(tenant)).Value?.ToString() ?? "------";
        }
    }
}
