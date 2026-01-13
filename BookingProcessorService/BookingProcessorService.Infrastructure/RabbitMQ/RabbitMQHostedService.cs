using BookingProcessorService.Application.Interface;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProcessorService.Infrastructure.RabbitMQ
{
    public class RabbitMQHostedService : IHostedService
    {
        private readonly IRabbitMQConsumer _consumer;

        public RabbitMQHostedService(IRabbitMQConsumer consumer)
        {
            _consumer = consumer;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.InitializeAsync();
            await _consumer.Consume();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();

            return Task.CompletedTask;
        }
    }
}
