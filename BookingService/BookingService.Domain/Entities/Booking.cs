using BookingService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Passenger> Passengers { get; set; } = new();
    }
}
