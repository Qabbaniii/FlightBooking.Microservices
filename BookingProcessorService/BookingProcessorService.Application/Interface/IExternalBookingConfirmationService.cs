using Shared.Contracts.DTOs;

namespace BookingProcessorService.Application.Interface
{
    public interface IExternalBookingConfirmationService
    {
        Task<BookingConfirmationResponse> ConfirmBookingAsync(Guid bookingId);
    }
}
