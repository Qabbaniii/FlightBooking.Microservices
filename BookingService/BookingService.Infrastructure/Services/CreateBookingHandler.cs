using BookingService.Application.DTOs;
using BookingService.Application.Interfaces;
using BookingService.Application.Repositories;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using Shared.Contracts.DTOs;


namespace BookingService.Infrastructure.Services
{
    public class CreateBookingHandler(IBookingRepository Repository, IFlightAvailabilityService AvailabilityService, IBookingPublisher Publisher): ICreateBookingHandler
    {

        public async Task<Guid> HandleAsync(CreateBookingRequest request)
        {
            var count = request.Passengers.Count;


            var AvailableResponse = await AvailabilityService
                .IsFlightAvailableAsync(request.FlightId, count);

            if (!AvailableResponse.IsAvailable)
                throw new Exception("Flight not available");

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                FlightId = request.FlightId,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Passengers = request.Passengers.Select(p => new Passenger
                {
                    Id = Guid.NewGuid(),
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList()
            };

            await Repository.AddAsync(booking);

            
            await Repository.SaveChangesAsync();

            await Publisher.PublishBookingCreatedAsync(new BookingCreatedEvent
            {
               BookingId = booking.Id,
               FlightId = booking.FlightId,
               PassengersCount = booking.Passengers.Count
            });

            return booking.Id;
        }
    }
}
