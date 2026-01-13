using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.DTOs
{
    public class CreateBookingRequest
    {
        public Guid FlightId { get; set; }
        public List<PassengerDto> Passengers { get; set; } = new();
    }
}
