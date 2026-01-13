using BookingProcessorService.Application.Interface;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProcessorService.Application.Handler
{
    public class BookingCreatedHandler(IExternalBookingConfirmationService ConfirmationService): IBookingCreatedHandler
    {
        public async Task HandleAsync(BookingCreatedEvent @event)
        {
            if (@event != null)
            {
                Console.WriteLine($"Received Booking Created Event: {@event.BookingId}");

                var confirmed = await ConfirmationService.ConfirmBookingAsync(@event.BookingId);
                if (confirmed.IsConfirmed)
                {
                    Console.WriteLine($"Booking {@event.BookingId} confirmed");
                }
                else
                {
                    Console.WriteLine($"Booking {@event.BookingId} failed to confirm");
                }
            }
        }
    }

}
