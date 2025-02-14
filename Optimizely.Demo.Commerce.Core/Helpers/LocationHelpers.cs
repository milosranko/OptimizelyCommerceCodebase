using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Http;

namespace Optimizely.Demo.Commerce.Core.Helpers;

public static class LocationHelpers
{
    public static IGeolocationResult GeoLocateCurrentRequest(HttpContext ctx)
    {
        var provider = ServiceLocator.Current.GetInstance<GeolocationProviderBase>();
        var ipString = ctx.Request.Query["ip"].FirstOrDefault() ?? ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? ctx.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";

        if (ipString.Contains(","))
        {
            var ips = ipString.Split(',').Where(x => !string.IsNullOrWhiteSpace(x) && x.Contains(".") && x.Length >= 7);

            if (ips.Any())
                ipString = ips.First().Trim(' ');
            else
                ipString = ctx.Connection.RemoteIpAddress.ToString() ?? "127.0.0.1";
        }

        IGeolocationResult result;

        try
        {
            // Remove the port from the IP address as Azure and other proxy servers/load balancers may add this in.
            if (ipString.Contains(".") && ipString.Contains(":"))
            {
                var uri = new Uri("http://" + ipString);
                ipString = uri.Host;
            }

            try
            {
                var ip = System.Net.IPAddress.Parse(ipString);
                result = provider.Lookup(ip);
            }
            catch (Exception ex)
            {
                result = null;
                ctx.Response.WriteAsync("<!-- IP: '" + ipString + "' -->").GetAwaiter().GetResult();
            }
        }
        catch (Exception ex)
        {
            result = null;
        }

        return result;
    }
}