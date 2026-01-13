using BookingProcessorService.Application.Handler;
using BookingProcessorService.Application.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Contracts.DTOs;
using System.Text;
using System.Text.Json;

namespace BookingProcessorService.Infrastructure.RabbitMQ
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private IConnection? connection;
        private readonly ConnectionFactory factory;
        private BookingCreatedHandler handler;

        public RabbitMQConsumer(BookingCreatedHandler handler)
        {          
            factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
            };
            this.handler = handler;
    }
        public async Task Consume()
        {
            connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            if (connection == null)
                throw new InvalidOperationException("RabbitMQ connection is not initialized.");

            await channel.ExchangeDeclareAsync(
                exchange: "booking-exchange",
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null);

            await channel.QueueDeclareAsync(
                queue: "booking-confirmed-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            await channel.QueueBindAsync(
                queue: "booking-confirmed-queue",
                exchange: "booking-exchange",
                routingKey:"");
            await channel.BasicQosAsync(0, 1, false);

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
            
            await channel.BasicConsumeAsync(
                queue: "booking-confirmed-queue",
                autoAck: false,
                consumer: consumer);

            consumer.ReceivedAsync += async (_, e) =>
            {
                var body = Encoding.UTF8.GetString(e.Body.ToArray());
                var message = JsonSerializer.Deserialize<BookingCreatedEvent>(body);
                Console.WriteLine($"Message reseved : {message}");
                await handler.HandleAsync(message!);
                await channel.BasicAckAsync(e.DeliveryTag, false);
            };
        }
        public async Task InitializeAsync()
        {
            connection = await factory.CreateConnectionAsync();
        }
        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}



