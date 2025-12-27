using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace ipify
{
    public class Program
    {
        private static string GetIpV6(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress;

            string ipv6 = "unknown";

            if (ip != null && ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                ipv6 = ip.ToString();
            }

            return ipv6;
        }

        private static string GetIpV4(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress;

            string ipv4 = "unknown";

            if (ip != null)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4 = ip.ToString();
                }
                else if (ip.IsIPv4MappedToIPv6)
                {
                    ipv4 = ip.MapToIPv4().ToString();
                }
            }

            return ipv4;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto;

                // Ignore docker network
                options.KnownIPNetworks.Add(
                    new System.Net.IPNetwork(IPAddress.Parse("172.17.0.0"), 16)
                );
            });

            var app = builder.Build();
            app.UseForwardedHeaders();

            app.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(GetIpV4(context));
            });

            app.MapGet("/v4", async context =>
            {
                await context.Response.WriteAsync(GetIpV4(context));
            });

            app.MapGet("/v6", async context =>
            {
                await context.Response.WriteAsync(GetIpV6(context));
            });

            app.Run();
        }
    }
}