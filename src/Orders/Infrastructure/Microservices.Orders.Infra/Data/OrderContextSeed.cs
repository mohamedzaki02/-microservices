using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Orders.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microservices.Orders.Infra.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrdersContext ctx, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                ctx.Database.Migrate();
                if (!ctx.Orders.Any())
                {
                    ctx.Orders.AddRange(GetPreconfiguredOrders());
                    await ctx.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                if (retryForAvailability > 0)
                {
                    retryForAvailability--;
                    loggerFactory.CreateLogger<OrderContextSeed>().LogError(ex.Message);
                    await OrderContextSeed.SeedAsync(ctx, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>() {
                new Order {
                    UserName = "zaki",
                    Address = "Radisson Blu",
                    CardName = "Mohamed Ismail",
                    CardNumber = "5555-9999",
                    City = "Dubai" ,
                    Country = "UAE",
                    CVV = 592,
                    TotalPrice = 157
                }
            };
        }
    }
}