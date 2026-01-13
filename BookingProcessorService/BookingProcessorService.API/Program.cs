using BookingProcessorService.Application.Handler;
using BookingProcessorService.Application.Interface;
using BookingProcessorService.Infrastructure.HttpClients;
using BookingProcessorService.Infrastructure.RabbitMQ;
using BookingProcessorService.Infrastructure.Service;


namespace BookingProcessorService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<BookingCreatedHandler>();

            builder.Services.AddSingleton<IBookingConsumer, BookingConsumer>();
            builder.Services.AddSingleton<IExternalBookingConfirmationService, ExternalBookingConfirmationService>();
            
            builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();
            builder.Services.AddHostedService<RabbitMQHostedService>();

            builder.Services.AddHttpClient<BookingProcessorServiceClient>(
            client =>
            {
                client.BaseAddress = new Uri("https://localhost:7135");
                client.Timeout = TimeSpan.FromSeconds(60);
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
