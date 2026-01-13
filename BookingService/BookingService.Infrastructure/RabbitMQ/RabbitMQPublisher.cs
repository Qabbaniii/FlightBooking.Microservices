using BookingService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private IConnection? connection;
        private readonly ConnectionFactory factory;

        public RabbitMQPublisher()
        {
            factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
                
            };
        }
        public async Task InitializeAsync()
        {
            connection = await factory.CreateConnectionAsync();
        }
        public async Task Publish<T>(T message)
        {
            using var channel = await connection.CreateChannelAsync();

            if (connection == null)
                throw new InvalidOperationException("RabbitMQ connection is not initialized.");

            var bodyJson = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(message));

            await channel.ExchangeDeclareAsync(
                exchange: "booking-exchange",
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null);

            await channel.QueueDeclareAsync(
                queue: "booking-created-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            await channel.QueueBindAsync(
                queue: "booking-created-queue",
                exchange: "booking-exchange",
                routingKey: "");

            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent,
                ContentType = "application/json"
            };

            await channel.BasicPublishAsync(
                exchange: "booking-exchange",
                routingKey: "",
                mandatory: false,
                basicProperties: properties,
                body: bodyJson);
            
        }
        public void Dispose()
        {
            connection?.Dispose();
        }

    }
}