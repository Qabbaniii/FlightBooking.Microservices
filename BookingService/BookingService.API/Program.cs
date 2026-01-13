using BookingService.Application.Interfaces;
using BookingService.Application.Repositories;
using BookingService.Infrastructure.Context;
using BookingService.Infrastructure.HttpClients;
using BookingService.Infrastructure.RabbitMQ;
using BookingService.Infrastructure.Repositories;
using BookingService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Booking")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IFlightAvailabilityService, FlightAvailabilityService>();
            builder.Services.AddScoped<IBookingPublisher, BookingPublisher>();
            builder.Services.AddScoped<ICreateBookingHandler, CreateBookingHandler>();
            builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();
            builder.Services.AddHostedService<RabbitMQHostedService>();

            builder.Services.AddHttpClient<FlightAvailabilityClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7135");
            });


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();
          
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
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
