// <copyright file="ConsoleLogsExporter.cs" company="Tricentis">
// Copyright 2024 Tricentis (https://www.tricentis.com/legal-information/contracts/)
// </copyright>

using OpenTelemetry;
using OpenTelemetry.Logs;

namespace Instrumentation;

/// <summary>
/// Exports logs to console.
/// </summary>
public class ConsoleLogsExporter : BaseExporter<LogRecord>
{
    public override ExportResult Export(in Batch<LogRecord> batch)
    {
        using var scope = SuppressInstrumentationScope.Begin();
        foreach (var logRecord in batch)
        {
            var logLevel = logRecord.LogLevel.ToString();
            var props = new[]
            {
                "Timestamp: " + DateTime.UtcNow.TimeOfDay,
                "Method: " + GetAttribute(logRecord, "request-method"),
                "ReqID: " + GetAttribute(logRecord, "request-id"),
                "LogLevel: " + (logLevel.Length == 5 ? logLevel : $"{logLevel.Substring(0, 4)} "),
                logRecord.FormattedMessage,
            };

            Console.WriteLine(String.Join("\t", props.ToArray()));
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
