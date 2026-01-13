using BookingService.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.RabbitMQ
{
    public class RabbitMQHostedService : IHostedService
    {
        private readonly IRabbitMQPublisher _publisher;

        public RabbitMQHostedService(IRabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _publisher.InitializeAsync();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_publisher is IDisposable disposable)
                disposable.Dispose();

            return Task.CompletedTask;
        }
    }
}
