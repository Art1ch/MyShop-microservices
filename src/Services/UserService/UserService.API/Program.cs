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
using UserService.API.Extensions;


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

            builder.AddDatabase();
            builder.AddRepository();
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
