using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.DTOs
{
    public class BookingCreatedEvent
    {
        public Guid BookingId { get; set; }
        public Guid FlightId { get; set; }
        public int PassengersCount { get; set; }
    }
}
