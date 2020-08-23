using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Orders.Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservices.Orders.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDatabase(host);
            host.Run();
        }

        private static async void SeedDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var ordersContext = services.GetRequiredService<OrdersContext>();
                    await OrderContextSeed.SeedAsync(ordersContext, loggerFactory);
                }
                catch (System.Exception ex)
                {
                    loggerFactory.CreateLogger<Program>().LogError(ex.Message);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
