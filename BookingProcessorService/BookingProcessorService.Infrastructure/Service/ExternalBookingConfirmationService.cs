using BookingProcessorService.Application.Interface;
using BookingProcessorService.Infrastructure.HttpClients;
using Shared.Contracts.DTOs;

namespace BookingProcessorService.Infrastructure.Service
{
    public class ExternalBookingConfirmationService(BookingProcessorServiceClient client)
    : IExternalBookingConfirmationService
    {
        public async Task<BookingConfirmationResponse> ConfirmBookingAsync(Guid bookingId)
        {
           return await client.ConfirmBooking(bookingId);
        }
    }
}
