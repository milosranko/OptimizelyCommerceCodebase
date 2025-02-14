using Microsoft.AspNetCore.Http;

namespace Optimizely.Demo.Cms.Core.Business.Initialization;

public class PreSendHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public PreSendHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-UA-Compatible", "IE=edge");
        context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

        if (!(context.Request.Query["act"].ToString() ?? string.Empty).Equals("logout") && !context.Response.Headers.ContainsKey("X-Frame-Options"))
        {
            context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
        }

        var requestPath = context.Request.Path.ToString().TrimStart('/');

        if (requestPath.StartsWith("~") || requestPath.StartsWith("assets", StringComparison.CurrentCultureIgnoreCase))
        {
            string ExpireDate = DateTime.UtcNow.AddDays(30).ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            context.Response.Headers.Expires = ExpireDate + " GMT";
            context.Response.Headers.CacheControl = "public, max-age=2592000";
        }

        await _next(context);
    }
}
