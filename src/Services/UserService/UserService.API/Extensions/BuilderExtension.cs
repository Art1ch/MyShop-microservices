using EventBus.EventBus.Implementations;
using EventBus.EventBus.Interface;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.MessageBrokerSettings;
using UserService.Application.Contracts.Repository;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repository;

namespace UserService.API.Extensions
{
    public static class BuilderExtension
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRepository(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
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

                options.RegisterServicesFromAssembly(typeof(UserService.Application.Commands.CreateUser.CreateUserCommandHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(UserService.Application.Commands.DeleteUser.DeleteUserCommandHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(UserService.Application.Commands.UpdateUser.UpdateUserCommandHandler).Assembly);

                options.RegisterServicesFromAssembly(typeof(UserService.Application.Queries.GetAll.GetAllQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(UserService.Application.Queries.GetUserById.GetUserByIdQueryHandler).Assembly);
                options.RegisterServicesFromAssembly(typeof(UserService.Application.Queries.GetUserByName.GetUserByNameQueryHandler).Assembly);
            });
        }

        public static void AddValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(typeof(UserService.Application.Commands.CreateUser.CreateUserCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(UserService.Application.Commands.UpdateUser.UpdateUserCommandValidator).Assembly);
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
