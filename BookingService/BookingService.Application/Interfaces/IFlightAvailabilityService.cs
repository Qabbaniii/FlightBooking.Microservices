using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface IFlightAvailabilityService
    {
        Task<AvailabilityResponse> IsFlightAvailableAsync(Guid flightId, int passengersCount);
    }
}
