using EventBus.EventBus.Implementations;
using EventBus.EventBus.Interface;
using MassTransit;
using Microsoft.Extensions.Options;
using Shared.MessageBrokerSettings;
using StoreService.Application.Contracts;
using StoreService.Infrastructure.Consumer;
using StoreService.Infrastructure.Context;
using StoreService.Infrastructure.Repositories;
using StoreService.Infrastructure.Settings;

namespace StoreService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<StoreContext>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IEventBus, EventBusImplementation>();

            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(Program).Assembly);

                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Commands.Product.CreateProduct.CreateProductCommandHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Commands.Product.DeleteProduct.DelteProductCommandHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Commands.Product.UpdateProduct.UpdateProductCommandHandler).Assembly);

                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Queries.Product.GetAll.GetAllQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Queries.Product.GetProductById.GetProductByIdQueryHandler).Assembly);


                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Commands.Basket.AddProductToBasket.AddProductToBasketCommandHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Commands.Basket.DeleteProductFromBasket.DeleteProductFromBasketCommandHandler).Assembly);

                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Queries.Basket.GetAll.GetAllQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(StoreService.Application.Queries.Basket.GetBasketById.GetBasketByIdQueryHandler).Assembly);

                options.RegisterServicesFromAssemblies(typeof(StoreService.Application.Commands.Basket.MakeOrder.MakeOrderCommandHandler).Assembly);
            });

            builder.Services.Configure<MessageBrokerSettings>(
                builder.Configuration.GetSection("MessageBrokerSettings"));
            builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            builder.Services.AddMassTransit((busConfigurator) =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<UserCreatedEventConsumer>();
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
