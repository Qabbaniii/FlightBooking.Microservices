using BookingProcessorService.Application.Handler;
using BookingProcessorService.Application.Interface;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingProcessorService.Infrastructure.Service
{
    public class BookingConsumer(IRabbitMQConsumer consumer) : IBookingConsumer
    {
        public async Task BookingCreatedConsumer()
        {
          await consumer.Consume();
        }

        
    }
}


