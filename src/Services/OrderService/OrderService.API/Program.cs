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

namespace OrderService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IEventBus, EventBusImplementation>();

            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);
                options.RegisterServicesFromAssembly(typeof(OrderService.Application.Queries.GetAll.GetAllQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(OrderService.Application.Queries.GetOrderById.GetOrderByIdQueryHandler).Assembly);
            });

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
