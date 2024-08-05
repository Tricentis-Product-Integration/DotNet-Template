using OpenTelemetry;
using OpenTelemetry.Logs;

namespace Instrumentation;

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
                logLevel.Length == 5 ? logLevel : $"{logLevel.Substring(0, 4)} ",
                logRecord.Attributes!.FirstOrDefault(pair => pair.Key.Equals("request-id")).Value?.ToString(),
            };
            Console.WriteLine(string.Join("\n", props));
            if (logRecord.Exception != null)
            {
                Console.WriteLine(logRecord.Exception);
            }
        }
        return ExportResult.Success;
    }
}