using EventBus.EventBus.Implementations;
using EventBus.EventBus.Interface;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Options;
using Shared.MessageBrokerSettings;
using StoreService.Application.Contracts;
using StoreService.Infrastructure.Consumer;
using StoreService.Infrastructure.Context;
using StoreService.Infrastructure.Repositories;
using StoreService.Infrastructure.Settings;

namespace StoreService.API.Extensions
{
    public static class BuilderExtension
    {
        public static void AddDatabases(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<StoreContext>();
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
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
        }
        public static void AddValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(typeof(StoreService.Application.Commands.Basket.AddProductToBasket.AddProductToBasketCommandValidation).Assembly);

            builder.Services.AddValidatorsFromAssembly(typeof(StoreService.Application.Commands.Product.CreateProduct.CreateProductCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(StoreService.Application.Commands.Product.UpdateProduct.UpdateProductCommandValidator).Assembly);
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
        }
    }
}
