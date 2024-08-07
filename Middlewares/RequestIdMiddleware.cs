using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Middlewares;

public class RequestIdMiddleware
{
    private const string RequestId = "RequestId";

    private readonly RequestDelegate next;

  
    public RequestIdMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string reqId = context.Request.Headers[RequestId].FirstOrDefault() ?? GetRandomString(8);

        if (!context.Request.Headers.ContainsKey(RequestId))
        {
            context.Request.Headers[RequestId] = reqId;
        }

        if (!context.Response.Headers.ContainsKey(RequestId))
        {
            context.Response.Headers[RequestId] = reqId;
        }

        context.Items.TryAdd(RequestId, reqId);

        await this.next(context);
    }

    /// <summary>
    /// Generates a random string.
    /// </summary>
    /// <param name="length">The length of the string.</param>
    /// <param name="possibleChars">The possible chars to be used.</param>
    /// <returns>string.</returns>
    private static string GetRandomString(int length,
        string possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        return new string(Enumerable.Repeat(possibleChars, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}