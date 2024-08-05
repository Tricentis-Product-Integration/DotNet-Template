using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Instrumentation;

public class LogBuilder
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogBuilder> _logger;

    public LogBuilder(RequestDelegate next, ILogger<LogBuilder> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        await LogRequest(context);

        var originalResponseBody = context.Response.Body;

        using var responseBody = new MemoryStream();
        {
            context.Response.Body = responseBody;
            await _next.Invoke(context);

            await LogResponse(context, responseBody, originalResponseBody);
        }
    }

    private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
    {
        var responseContent = new StringBuilder();

        responseContent.AppendLine("=== Response Info ===");
        responseContent.AppendLine($"Timestamp = {DateTime.UtcNow.TimeOfDay}");
        responseContent.AppendLine($"Method = {context.Request.Method.ToUpper()}");
        responseContent.AppendLine($"TraceID = {context.TraceIdentifier}");
        //responseContent.AppendLine($"RequestID = {responseBody.}")
        _logger.LogInformation(responseContent.ToString());
    }

    private async Task LogRequest(HttpContext context)
    {
        var requestContent = new StringBuilder();

        requestContent.AppendLine("=== Request Info ===");
        requestContent.AppendLine($"Timestamp = {DateTime.UtcNow.TimeOfDay}");
        requestContent.AppendLine($"Method = {context.Request.Method.ToUpper()}");
        requestContent.AppendLine($"TraceID = {context.TraceIdentifier}");

        _logger.LogInformation(requestContent.ToString());
    }
}