using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x =>
                    {
                        x.UseKestrel();
                        x.UseStartup<Startup>();
                    })
                .UseSerilog((hostingContext, services, x) => x.ReadFrom.Configuration(hostingContext.Configuration));
    }
}
