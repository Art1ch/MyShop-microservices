using Microsoft.EntityFrameworkCore;
using UserService.Application.Contracts.Repository;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repository;
using MediatR;
using MassTransit;
using Shared.MessageBrokerSettings;
using EventBus.EventBus.Interface;
using EventBus.EventBus.Implementations;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;


namespace UserService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IEventBus, EventBusImplementation>();
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
