using BookingService.Application.Interfaces;
using BookingService.Infrastructure.HttpClients;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Services
{
    public class FlightAvailabilityService(FlightAvailabilityClient client) : IFlightAvailabilityService
    {
        public async Task<AvailabilityResponse> IsFlightAvailableAsync(Guid flightId, int passengersCount)
        {
            return await client.IsFlightAvailableAsync(flightId, passengersCount);
        }
    }

}
