using BookingService.Application.Interfaces;
using RabbitMQ.Client;
using Shared.Contracts.DTOs;
using System.Text;
using System.Text.Json;

public class BookingPublisher(IRabbitMQPublisher MQPublisher) : IBookingPublisher
{
    public async Task PublishBookingCreatedAsync(BookingCreatedEvent @event)
    {
        await MQPublisher.Publish<BookingCreatedEvent>(@event);
    }

   
}
