using System;
using System.IO;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bridge.Unique.Profile.API
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Host terminated unexpectedly {DateTime.UtcNow:yyyy-MM-dd.HH:mm:ss}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options =>
                        {
                            options.ConfigureHttpsDefaults(httpsOptions =>
                            {
                                httpsOptions.SslProtocols = SslProtocols.Tls12;
                            });
                        })
                        .CaptureStartupErrors(true)
                        .UseStartup<Startup>();
                });
        }
    }
#pragma warning restore CS1591
}