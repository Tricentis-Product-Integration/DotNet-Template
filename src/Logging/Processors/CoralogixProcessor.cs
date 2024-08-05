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

        var context = this.contextAccessor.HttpContext;
        if (context != null)
        {
            if (context.Items.TryGetValue("RequestId", out var req))
            {
                reqId = req!.ToString()!;
            }
        }


        var attributes = new List<(string Key, string? Value)>
        {
            ("request-id", reqId),
        };

        this.AddAttributes(data, attributes.ToArray());
        base.OnEnd(data);
    }


    protected abstract void AddAttributes(T data, params (string Key, string? Value)[] attributes);
}
