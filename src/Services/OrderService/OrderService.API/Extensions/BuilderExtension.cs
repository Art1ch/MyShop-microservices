using EventBus.EventBus.Implementations;
using EventBus.EventBus.Interface;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Application.Contracts;
using OrderService.Infrastructure.Consumer;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repository;
using Shared.MessageBrokerSettings;

namespace OrderService.API.Extensions
{
    public static class BuilderExtension
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<OrderContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void AddEventBus(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IEventBus, EventBusImplementation>();
        }

        public static void AddMediatR(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);
                options.RegisterServicesFromAssembly(typeof(OrderService.Application.Queries.GetAll.GetAllQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(OrderService.Application.Queries.GetOrderById.GetOrderByIdQueryHandler).Assembly);
            });
        }

        public static void AddMessageBroker(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageBrokerSettings>(
                builder.Configuration.GetSection("MessageBrokerSettings"));
            builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            builder.Services.AddMassTransit((busConfigurator) =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<MakeOrderEventConsumer>();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();
                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}
