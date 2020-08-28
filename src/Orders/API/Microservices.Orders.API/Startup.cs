using AutoMapper;
using Microservices.Orders.Core.Repositories.Base;
using Microservices.Orders.Infra.Repositories;
using Microservices.Orders.Infra.Data;
using Microservices.Orders.Infra.Repositories.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microservices.Orders.Core.Repositories;
using Microsoft.OpenApi.Models;
using MediatR;
using Microservices.Orders.Application.CommandHandlers;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using EventBusRabbitMQ.Producers;
using Microservices.Orders.API.RabbitMQ;
using Microservices.Orders.API.Extensions;

namespace Microservices.Orders.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<OrdersContext>(opts =>
                                                    opts.UseSqlServer(Configuration.GetConnectionString("OrdersConnection")),
                                                    ServiceLifetime.Singleton);



            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrdersRepository));
            services.AddTransient<IOrderRepository, OrdersRepository>();

            services.AddAutoMapper(typeof(Startup));

            // services.AddMediatR(typeof(BasketCheckoutCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(BasketCheckoutCommandHandler).Assembly);

            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders Microservice Docs v1", Version = "v1" });
            });

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory() { HostName = Configuration["EventBus:HostName"] };
                var username = Configuration["EventBus:UserName"];
                var password = Configuration["EventBus:Password"];
                if (!string.IsNullOrEmpty(username)) factory.UserName = username;
                if (!string.IsNullOrEmpty(password)) factory.Password = password;
                return new RabbitMQConnection(factory);
            });

            services.AddSingleton<EventBusRabbitMQConsumer>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Microservice v1");
            });

            app.UseRabbitMQListener();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
