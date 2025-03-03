using EventBus.EventBus.Implementations;
using EventBus.EventBus.Interface;
using MassTransit;
using Microsoft.Extensions.Options;
using Shared.MessageBrokerSettings;
using StoreService.API.Extensions;
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

            builder.AddDatabases();
            builder.AddRepositories();
            builder.AddEventBus();
            builder.AddMediatR();
            builder.AddMessageBroker();

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
